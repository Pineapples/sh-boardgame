using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;
using SecretHitler.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretHitler.API.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPolicyRepository _policyRepository;

        public GameService(IGameRepository gameRepository, IPolicyRepository policyRepository)
        {
            this._gameRepository = gameRepository;
            this._policyRepository = policyRepository;
        }

        public Game StartGame(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            if (game == null)
            {
                throw new EntityNotFoundException<Game>(gameId);
            }

            AssignRoles(game.Players);
            game.GameStateId = GameState.ChoosePresident;
            game.ChoiceRounds.Add(new ChoiceRound());
            _gameRepository.Update(game);
            return game;
        }

        public Policy DrawPolicy(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            if(game == null)
            {
                throw new EntityNotFoundException<Game>(gameId);
            }

            var policiesLeft = Settings.PolicyDeckAmount - game.Policies.Count();
            var liberalPoliciesLeft = Settings.LiberalPolicyAmount - game.Policies.Where(x => x.PolicyType == PolicyType.Liberal).Count();
            Random rnd = new Random();
            var policyType = rnd.NextDouble() < (liberalPoliciesLeft / policiesLeft) ? PolicyType.Liberal : PolicyType.Facist;

            var policy = new Policy(policyType);
            policy.GameId = gameId;
            policy = _policyRepository.Add(policy);
            return policy;
        }

        private void AssignRoles(IEnumerable<Player> players)
        {        
            int fascistCount = 0;
            switch (players.Count())
            {
                case 5: 
                case 6:
                    fascistCount = 1;
                    break;
                case 7:
                case 8:
                    fascistCount = 2;
                    break;
                case 9:
                case 10:
                    fascistCount = 3;
                    break;
                default:
                    throw new Exception("Player count is less than 5 or more than 10");
            }   

            for(int i = 0; i < fascistCount; i++)
            {
                AssignRole(players.Where(x => x.Role == null), RoleType.Fascist); 
            }

            AssignRole(players.Where(x => x.Role == null), RoleType.Hitler);
            var count = players.Where(x => x.Role == null).Count();
            for (int i = 0; i < count; i++)
            {
                AssignRole(players.Where(x => x.Role == null), RoleType.Liberal);
            }
        }


        private void AssignRole(IEnumerable<Player> playersToAssign, RoleType role)
        {
            Random rnd = new Random();
            var index = rnd.Next(0, playersToAssign.Count() - 1);
            playersToAssign.ElementAt(index).Role = role;
        }

    }
}
