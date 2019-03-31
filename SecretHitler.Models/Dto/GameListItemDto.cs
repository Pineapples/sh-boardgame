using System;
using System.Collections.Generic;
using SecretHitler.Models.Entities;

namespace SecretHitler.Models.Dto
{
    public class GameListItemDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public GameState GameStateId { get; set; }
        public List<PlayerDto> Players { get; set; }
    }
}
