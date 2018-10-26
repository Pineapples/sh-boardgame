using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecretHitler.Models.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace SecretHitler.API.Repositories
{
    public class GameRepository : IGameRepository
    {
        private const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        private readonly DataContext _context;
        private static Random random = new Random();

        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public Game Add(Game game)
        {
            game.JoinKey = GenerateJoinKey();
            game.GameStateId = GameState.Open;
            _context.Add(game);
            _context.SaveChanges();
            return game;
        }

        public void Delete(int gameId)
        {
            throw new NotImplementedException();
        }

        public Game Get(int gameId)
        {
            return _context.Games.Include(x => x.Players).FirstOrDefault(x => x.Id == gameId);
        }

        public List<Game> GetAll()
        {
            return _context.Games.Include(x => x.Players).ToList();
        }

        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
        public Game GetByJoinKey(string joinKey)
        {
            return _context.Games.Where(x => x.JoinKey == joinKey).AsNoTracking().FirstOrDefault();
        }

        public Game GetByJoinKeyWithPlayers(string joinKey)
        {
            return _context.Games.Where(x => x.JoinKey == joinKey).Include(x => x.Players).FirstOrDefault();
        }

        private string GenerateJoinKey()
        {
            var length = 8;
            var exists = true;
            string key = "";
            while (exists)
            {
                key = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());

                if(!_context.Games.Any(x => x.JoinKey == key))
                {
                    exists = false;
                }
            }
            return key;
        }

    }
}
