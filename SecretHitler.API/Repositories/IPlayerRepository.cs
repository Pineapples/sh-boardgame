using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.Repositories
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Delete(int playerId);
        Player Get(int playerId);
        Player GetPlayerByName(string userName, int gameId);
        List<Player> GetAll();
        void Update(Player player);
    }
}
