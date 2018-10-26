using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Hubs
{
    public class GameHub : Hub
    {
        public Task Send(string data)
        {
            return Clients.All.InvokeAsync("Send", data);
        }

        public Task PlayerJoined(List<Player> players)
        {
            return Clients.All.InvokeAsync("PlayerJoined", players);
        }
    }
}
