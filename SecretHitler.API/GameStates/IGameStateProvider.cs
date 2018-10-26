using SecretHitler.API.Services;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.GameStates
{
    public interface IGameStateProvider
    {
        IGameState Get(GameState gameState);
    }
}