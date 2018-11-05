using System;
using System.Collections.Generic;
using System.Linq;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Services
{
    public class GameService : IGameService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameDataService _gameDataService;

        public GameService(IPolicyRepository policyRepository,
                           IPlayerRepository playerRepository,
                           IGameDataService gameDataService)
        {
            this._policyRepository = policyRepository;
            this._playerRepository = playerRepository;
            this._gameDataService = gameDataService;
        }

        public Game CreateGame() {
            var game = new Game
            {
                GameStateId = GameState.Open,

            };
            return _gameDataService.AddGame(game);
        }

        public Game ViewGame(string joinKey) {
            return _gameDataService.GetGameWithPlayers(joinKey);
        }

        public Player JoinGame(string joinKey, string userName) {
            var game = _gameDataService.GetGame(joinKey);
            _playerRepository.Add(new Player
            {
                UserName = userName,
                GameId = game.Id
            });
            return _playerRepository.GetPlayerByName(userName, game.Id);
        }

        public Game StartGame(int gameId)
        {
            var game = _gameDataService.GetGame(gameId);

            AssignRoles(game.Players);
            game.GameStateId = GameState.ChoosePresident;
            game.ChoiceRounds.Add(new ChoiceRound());
            _gameDataService.UpdateGame(game);
            return game;
        }

        public Policy DrawPolicy(int gameId)
        {
            var game = _gameDataService.GetGame(gameId);

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
