using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<VoteRound> VoteRounds { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<ChoiceRound> ChoiceRounds { get; set; }
        public DbSet<Choice> Choices { get; set; }
    }
}
