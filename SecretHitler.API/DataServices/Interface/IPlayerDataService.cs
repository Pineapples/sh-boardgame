using System;
namespace SecretHitler.API.DataServices.Interface
{
    public interface IPlayerDataService
    {
        void AddConnectionId(int playerId, string connectionId);
    }
}
