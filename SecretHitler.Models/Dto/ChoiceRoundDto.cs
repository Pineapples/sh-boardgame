using System;
using System.Collections.Generic;

namespace SecretHitler.Models.Dto
{
    public class ChoiceRoundDto
    {
        public DateTime DateCreated { get; set; }
        public List<ChoiceDto> Choices { get; set; }
    }
}