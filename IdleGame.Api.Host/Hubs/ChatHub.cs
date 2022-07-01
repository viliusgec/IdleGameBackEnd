using Microsoft.AspNetCore.SignalR;
using System;

namespace IdleGame.Api.Host.Hubs
{
    public class ChatHub : Hub
    {
        /*public async Task NewMessage(long username, string message) =>
            Console.WriteLine(username.ToString() + ": " + message);
        await Clients.All.SendAsync("messageReceived", username, message);*/
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
