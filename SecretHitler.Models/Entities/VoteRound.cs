using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("VoteRound")]
    public class VoteRound
    {
        public VoteRound()
        {
            DateCreated = DateTime.Now;
        }

        public int Id { get; set; }
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
