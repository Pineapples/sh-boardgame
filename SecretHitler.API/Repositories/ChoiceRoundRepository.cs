using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Repositories
{
    public class ChoiceRoundRepository : IChoiceRoundRepository
    {
        private readonly DataContext _context;

        public ChoiceRoundRepository(DataContext context)
        {
            _context = context;
        }

        public ChoiceRound GetLatestChoiceRoundByGameId(int gameId) {
            return _context.ChoiceRounds
                            .Where(x => x.GameId == gameId)
                            .Include(x => x.Choices)
                            .Include(x => x.Game)
                            .OrderByDescending(x => x.DateCreated)
                            .FirstOrDefault();
        }
    }
}
