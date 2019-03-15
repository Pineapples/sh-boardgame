using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IPlayerDataService _playerDataService;

        public GameHub(IHubContext<GameHub> context, IPlayerDataService playerDataService)
        {
            this._context = context;
            this._playerDataService = playerDataService;
        }

        public Task Send(string method, object data)
        {
            return this._context.Clients.All.SendAsync(method, data);
        }

        public Task Send(string method, object data, int gameId) {
            return this._context.Clients.Group(gameId.ToString()).SendAsync(method, data);
        }

        public Task JoinGame(int playerId) {
            var connectionId = Context.ConnectionId;
            _playerDataService.AddConnectionId(playerId, connectionId);
            var player = _playerDataService.GetPlayerById(playerId);
            return this._context.Groups.AddToGroupAsync(connectionId, player.GameId.ToString());
        }
    }
}
