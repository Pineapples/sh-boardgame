using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.GameStates
{
    public class GameStateProvider : IGameStateProvider
    {
        private readonly IServiceProvider serviceProvider;

        public GameStateProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IGameState Get(GameState gameState)
        {
            var type = GameStateTypeProvider.Get(gameState);
            return (IGameState)serviceProvider.GetService(type);
        }
    }
}
