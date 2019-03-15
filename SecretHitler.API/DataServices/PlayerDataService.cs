using System;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.DataServices
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerDataService(IPlayerRepository playerRepository)
        {
            this._playerRepository = playerRepository;
        }

        public void AddConnectionId(int playerId, string connectionId)
        {
            var player = this._playerRepository.Get(playerId);
            player.ConnectionId = connectionId;
            this._playerRepository.Update(player);
        }

        public Player GetPlayerById(int playerId) {
            return this._playerRepository.Get(playerId);
        }
    }
}
