using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("Game")]
    public class Game
    {
        public Game()
        {
            DateCreated = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public GameState GameStateId { get; set; }
        public ICollection<VoteRound> VoteRounds { get; set; }
        public ICollection<ChoiceRound> ChoiceRounds { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Policy> Policies { get; set; }
        public string JoinKey { get; set; }
    }
}
