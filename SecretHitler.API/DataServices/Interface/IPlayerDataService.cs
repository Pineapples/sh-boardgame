using System;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.DataServices.Interface
{
    public interface IPlayerDataService
    {
        void AddConnectionId(int playerId, string connectionId);
        Player GetPlayerById(int playerId);
    }
}
