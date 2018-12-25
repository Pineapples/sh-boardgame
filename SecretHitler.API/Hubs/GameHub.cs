using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public Task Send(string method, object data)
        {
            return this._context.Clients.All.SendAsync(method, data);
        }
    }
}
