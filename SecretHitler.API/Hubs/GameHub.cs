using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly IHubContext<GameHub> _context;

        public GameHub(IHubContext<GameHub> context)
        {
            this._context = context;
        }

        public Task Send(string method, string data)
        {
            return this._context.Clients.All.SendAsync(method, data);
        }

        public Task PlayerJoined(IEnumerable<Player> players)
        {
            return Send("PlayerJoined", JsonConvert.SerializeObject(players, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public Task GameInfo(Game game) {
            return Send("GameInfo", JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}
