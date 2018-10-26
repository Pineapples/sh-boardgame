using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("Vote")]
    public class Vote
    {
        public Vote()
        {
            DateCreated = DateTime.Now;
        }
        public int Id { get; set; }
        public int VoterId { get; set; }
        [ForeignKey(nameof(VoterId))]
        public Player Voter { get; set; }
        public bool InFavor { get; set; }
        public int VoteRoundId { get; set; }
        [ForeignKey(nameof(VoteRoundId))]
        public VoteRound VoteRound { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
