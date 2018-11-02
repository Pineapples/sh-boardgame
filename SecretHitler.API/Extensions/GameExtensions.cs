using System;
using System.Linq;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Extensions
{
    public static class GameExtensions
    {
        public static bool HasPlayer(this Game game, int playerId) {
            return game.Players.Any(x => x.Id == playerId);
        }
    }
}
