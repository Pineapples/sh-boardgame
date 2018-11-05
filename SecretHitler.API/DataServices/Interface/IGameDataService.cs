using System;
using System.Collections.Generic;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.DataServices.Interface
{
    public interface IGameDataService
    {
        List<Game> GetOpenGames();
        Game GetGame(int gameId);
        Game GetGame(string joinKey);
        Game GetGameWithPlayers(string joinKey);
        Game AddGame(Game game);
        void UpdateGame(Game game);
    }
}
