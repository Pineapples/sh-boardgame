using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.GameStates
{
    public class GameStateTypeProvider
    {
        private GameStateTypeProvider() { }

        static readonly Dictionary<GameState, Type> _dict = new Dictionary<GameState, Type>();

        public static Type Get(GameState gameState)
        {
            Type type = null;
            if (_dict.TryGetValue(gameState, out type))
            {
                return type;
            }

            throw new ArgumentException("No type registered for this gameState");
        }

        public static void Register<Tderived>(GameState gameState)
        {
            var type = typeof(Tderived);
            _dict.Add(gameState, type);
        }
    }
}
