using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("Choice")]
    public class Choice
    {
        public Choice()
        {
            DateCreated = DateTime.Now;
        }

        public int Id { get; set; }
        public int ChooserId { get; set; }
        [ForeignKey(nameof(ChooserId))]
        public Player Chooser { get; set; }
        public int ChosenId { get; set; }
        [ForeignKey(nameof(ChosenId))]
        public Player Chosen { get; set; }
        public int ChoiceRoundId { get; set; }
        [ForeignKey(nameof(ChoiceRoundId))]
        public ChoiceRound ChoiceRound { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
