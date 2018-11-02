using System;
using System.Collections.Generic;
using System.Linq;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;
using SecretHitler.Models.Exceptions;

namespace SecretHitler.API.DataServices
{
    public class GameDataService : IGameDataService
    {
        private readonly IGameRepository _gameRepository;

        public GameDataService(IGameRepository gameRepository)
        {
            this._gameRepository = gameRepository;
        }

        public List<Game> GetOpenGames()
        {
            return _gameRepository.GetAll().Where(x => x.GameStateId == GameState.Open).ToList();
        }

        public Game GetGame(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            if (game == null)
            {
                throw new EntityNotFoundException<Game>(gameId);
            }
            return game;
        }

        public Game GetGame(string joinKey) {
            var game = _gameRepository.GetByJoinKey(joinKey);
            if (game == null)
            {
                throw new EntityNotFoundException<Game>();
            }
            return game;
        }

        public Game GetGameWithPlayers(string joinKey) {
            var game = _gameRepository.GetByJoinKeyWithPlayers(joinKey);
            if (game == null)
            {
                throw new EntityNotFoundException<Game>();
            }
            return game;
        }

        public void UpdateGame(Game game) {
            _gameRepository.Update(game);
        }

        public Game AddGame(Game game) {
            return _gameRepository.Add(game);
        }
    }
}
