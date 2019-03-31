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
    public class GameHub : Hub, IGameHub
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IPlayerDataService _playerDataService;

        public GameHub(IHubContext<GameHub> context, IPlayerDataService playerDataService)
        {
            this._context = context;
            this._playerDataService = playerDataService;
        }

        public async Task Send(string method, object data)
        {
            await this._context.Clients.All.SendAsync(method, data);
        }

        public async Task SendToGroup(string method, object data, int gameId) {
            await this._context.Clients.Group(gameId.ToString()).SendAsync(method, data);
        }

        public async Task SendToPlayer(string method, object data, string connectionId) {
            await this._context.Clients.Client(connectionId).SendAsync(method, data);

        }

        public async Task JoinGame(int playerId) {
            var connectionId = Context.ConnectionId;
            _playerDataService.AddConnectionId(playerId, connectionId);
            var player = _playerDataService.GetPlayerById(playerId);
            await this._context.Groups.AddToGroupAsync(connectionId, player.GameId.ToString());
        }
    }
}
