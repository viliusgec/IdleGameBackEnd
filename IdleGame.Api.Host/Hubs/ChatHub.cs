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
        private static Dictionary<string, string> playerLobbies = new Dictionary<string, string>();
        private static Dictionary<string, int> playerHealth = new Dictionary<string, int>();
        private static string[] playerList = {};
        public async Task joinLobby(string username, string lobbyId)
        {
            // Make 2 players max
            playerLobbies[username] = lobbyId;
            playerHealth[username] = 100; // Set initial health

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            await Clients.OthersInGroup(lobbyId).SendAsync("playerJoined", username);
        }

        public async Task createLobby(string username)
        {
            var random = new Random();
            var lobbyId = random.Next(0, 9999).ToString();
            // Make 2 players max
            playerHealth[username] = 100;
            playerLobbies[username] = lobbyId;

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            await Clients.Others.SendAsync("newLobbyCreated", playerLobbies);
            await Clients.Caller.SendAsync("lobbyCreated", lobbyId);
            // await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            // await Clients.Group(lobbyId).SendAsync("playerJoined", username);
        }
        public async Task getLobbies()
        {
            await Clients.Caller.SendAsync("newLobbyCreated", playerLobbies);
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
                await Clients.Group(lobbyId).SendAsync("gameOver", Context.ConnectionId);
            }
        }

        public async Task startGame(string username, string lobbyId)
        {
            await Clients.Group(lobbyId).SendAsync("gameStarted", username);
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
