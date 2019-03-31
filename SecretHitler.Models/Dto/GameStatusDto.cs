using System;
using System.Collections.Generic;
using SecretHitler.Models.Entities;

namespace SecretHitler.Models.Dto
{
    public class GameStatusDto
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public GameState GameStateId { get; set; }
        public List<VoteRoundDto> VoteRounds { get; set; }
        public List<ChoiceRoundDto> ChoiceRounds { get; set; }
        public List<PlayerDto> Players { get; set; }
        public List<PolicyDto> Policies { get; set; }
        public string JoinKey { get; set; }
    }
}
