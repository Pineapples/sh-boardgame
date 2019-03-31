using System;
using System.Collections.Generic;

namespace SecretHitler.Models.Dto
{
    public class VoteRoundDto
    {
        public DateTime DateCreated { get; set; }
        public List<VoteDto> Votes { get; set; }
    }
}
