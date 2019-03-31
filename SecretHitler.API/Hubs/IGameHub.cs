using System.Threading.Tasks;

namespace SecretHitler.API.Hubs
{
    public interface IGameHub
    {
        Task Send(string method, object data);
        Task SendToGroup(string method, object data, int gameId);
        Task SendToPlayer(string method, object data, string connectionId);
        Task JoinGame(int playerId);
    }
}