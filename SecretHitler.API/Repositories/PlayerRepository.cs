using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretHitler.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SecretHitler.API.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add(Player player)
        {
            _context.Add(player);
            _context.SaveChanges();
        }

        public void Delete(int playerId)
        {
            throw new NotImplementedException();
        }

        public Player Get(int playerId)
        {
            return _context.Players.Find(playerId);
        }

        public Player GetPlayerByName(string userName, int gameId)
        {
            return _context.Players.Include(x => x.Game).FirstOrDefault(x => x.UserName == userName && x.GameId == gameId);
        }

        public List<Player> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Player player)
        {
            var entry = this._context.Entry(player);
            entry.CurrentValues.SetValues(player);
            this._context.SaveChanges();
        }
    }
}
