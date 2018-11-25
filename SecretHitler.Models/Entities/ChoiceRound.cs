using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("ChoiceRound")]
    public class ChoiceRound
    {
        public ChoiceRound()
        {
            DateCreated = DateTime.Now;
        }

        public int Id { get; set; }
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
        public DateTime? DateCreated { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }
}
