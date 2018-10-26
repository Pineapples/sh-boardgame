using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("Player")]
    public class Player
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
        public RoleType? Role { get; set; }
    }
}
