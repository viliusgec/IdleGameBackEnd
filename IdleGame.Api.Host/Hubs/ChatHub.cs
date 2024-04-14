using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using IdleGame.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Reflection;

namespace IdleGame.Api.Host.Hubs
{
    public class ChatHub : Hub
    {
        /*public async Task NewMessage(long username, string message) =>
            Console.WriteLine(username.ToString() + ": " + message);
        await Clients.All.SendAsync("messageReceived", username, message);*/
        public class LobbyInfo
        {
            public string lobbyId { get; set; }
            public int money { get; set; }
        }
        private readonly IPlayerService _playerService;
        private readonly IPvPService _pvPService;
        private readonly IMappingRetrievalService _mappingService;

        private static Dictionary<string, string> playerLobbies = new Dictionary<string, string>();
        private static Dictionary<string, LobbyInfo> newPlayerLobbies = new Dictionary<string, LobbyInfo>();
        private static Dictionary<string, int> playerHealth = new Dictionary<string, int>();
        private static string[] playerList = {};

        public ChatHub(IPlayerService playerService, IPvPService pvpService, IMappingRetrievalService mappingService)
        {
            _pvPService = pvpService;
            _playerService = playerService;
            _mappingService = mappingService;
        }

        public async Task joinLobby(string username, string lobbyId)
        {
            // Make 2 players max
            playerLobbies[username] = lobbyId;
            newPlayerLobbies[username] = new LobbyInfo { lobbyId = lobbyId, money = 0 };
            playerHealth[username] = 100; // Set initial health

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            await Clients.OthersInGroup(lobbyId).SendAsync("playerJoined", username);
        }

        public async Task createLobby(string username, int money)
        {
            var random = new Random();
            var lobbyId = random.Next(0, 999999).ToString();
            var playerInfo = await _playerService.GetPlayer(username);
            // Make 2 players max
            playerHealth[username] = 100;
            playerLobbies[username] = lobbyId;
            newPlayerLobbies[username] = new LobbyInfo { lobbyId = lobbyId, money = money };

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            await Clients.Others.SendAsync("newLobbyCreated", playerLobbies);
            await Clients.Others.SendAsync("newLobbyWithMoneyCreated", newPlayerLobbies);
            await Clients.Caller.SendAsync("lobbyCreated", lobbyId);
        }
        public async Task getLobbies()
        {
            await Clients.Caller.SendAsync("newLobbyCreated", playerLobbies);
            await Clients.Caller.SendAsync("newLobbyWithMoneyCreated", newPlayerLobbies);
        }

        public async Task attack(string username)
        {
            string lobbyId = playerLobbies[username];

            var random = new Random();
            int damage = random.Next(1, 10); // You can adjust the damage logic as needed

            playerHealth[username] -= damage;

            await Clients.OthersInGroup(lobbyId).SendAsync("attackReceived", playerHealth[username]);
            await Clients.Caller.SendAsync("attackCompleted", playerHealth[username]);


            // Check if the game is over (e.g., health <= 0)
            if (playerHealth[username] <= 0)
            {
                var pvp = await _pvPService.GetPvP(int.Parse(lobbyId));
                pvp.Winner = !pvp.PlayerOne.Equals(username) ? pvp.PlayerTwo : pvp.PlayerOne;
                await _pvPService.UpdatePvP(_mappingService.Map<PvPDto>(pvp));

                await Clients.Group(lobbyId).SendAsync("gameOver", Context.ConnectionId);
            }
        }

        public async Task startGame(string username, string lobbyId)
        {
            await Clients.Group(lobbyId).SendAsync("gameStarted", username);
            var enemy = "";
            foreach (var lobby in newPlayerLobbies) {
                if (lobby.Value.lobbyId == lobbyId && !lobby.Key.Equals(username))
                    enemy = lobby.Key;
                    
            }
            await _pvPService.CreatePvP(new Contracts.PvPDto { Id = int.Parse(lobbyId), PlayerOne = username, PlayerTwo = enemy, Bet = newPlayerLobbies[username].money, Winner = "" });
        }

        public async Task heal(string username)
        {
            string lobbyId = playerLobbies[username];
            double missingHP = (100 - playerHealth[username]);
            int hpToAdd = (int)(missingHP * 0.8);
            playerHealth[username] += hpToAdd;

            await Clients.OthersInGroup(lobbyId).SendAsync("enemyHealed", playerHealth[username]);
            await Clients.Caller.SendAsync("healingCompleted", playerHealth[username]);
        }
    }
}
