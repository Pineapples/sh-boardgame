using System;
using AutoMapper;
using SecretHitler.Models.Dto;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Mappers
{
    public static class Mappers
    {
        public static void InitializeMappers(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Game, GameStatusDto>();
            cfg.CreateMap<ChoiceRound, ChoiceRoundDto>();
            cfg.CreateMap<Player, PlayerDto>();
            cfg.CreateMap<Player, PlayerInfoDto>();
            cfg.CreateMap<Policy, PolicyDto>();
            cfg.CreateMap<VoteRound, VoteRoundDto>();

            cfg.CreateMap<Choice, ChoiceDto>()
            .ForMember(d => d.Chooser, o => o.MapFrom(s => s.Chooser.UserName))
            .ForMember(d => d.Chosen, o => o.MapFrom(s => s.Chosen.UserName));

            cfg.CreateMap<Vote, VoteDto>()
            .ForMember(d => d.Voter, o => o.MapFrom(s => s.Voter.UserName));
        }
    }
}
