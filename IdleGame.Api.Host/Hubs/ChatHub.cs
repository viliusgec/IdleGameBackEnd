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
            await Clients.Group(lobbyId).SendAsync("playerJoined", username);
        }

        public async Task createLobby(string username)
        {
            var random = new Random();
            var lobbyId = random.Next(0, 9999).ToString();
            // Make 2 players max
            playerHealth[username] = 100;
            playerLobbies[username] = lobbyId;
            // Context.Items
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
            int opponentHealth = playerHealth[username];

            var random = new Random();
            int damage = random.Next(1, 10); // You can adjust the damage logic as needed

            opponentHealth -= damage;

            await Clients.Group(lobbyId).SendAsync("attackReceived", username, damage);
            await Clients.Caller.SendAsync("attackCompleted", opponentHealth);

            playerHealth[username] = opponentHealth;

            // Check if the game is over (e.g., health <= 0)
            if (opponentHealth <= 0)
            {
                await Clients.Group(lobbyId).SendAsync("gameOver", Context.ConnectionId);
            }
        }

        public async Task StartGameV2(string username, string lobbyId)
        {
            await Clients.Group(lobbyId).SendAsync("gameStarted", username);
        }

        public async Task newAttack(int hp)
        {
            var a = new Random();
            hp = a.Next(0, hp);
            await Clients.Others.SendAsync("attackRecieved", hp);
            await Clients.Caller.SendAsync("attackCompleted", hp);
        }

        public async Task startGame(string username)
        {
            await Clients.All.SendAsync("gameStarted", username);   
        }
    }
}
