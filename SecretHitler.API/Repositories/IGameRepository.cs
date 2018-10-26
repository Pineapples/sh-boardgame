using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.Repositories
{
    public interface IGameRepository
    {
        Game Add(Game game);
        void Delete(int gameId);
        Game Get(int gameId);
        List<Game> GetAll();
        void Update(Game game);
        Game GetByJoinKey(string joinKey);
        Game GetByJoinKeyWithPlayers(string joinKey);
    }
}
