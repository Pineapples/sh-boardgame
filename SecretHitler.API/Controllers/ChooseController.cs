using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretHitler.API.Repositories;
using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using SecretHitler.Models.Exceptions;
using SecretHitler.API.GameStates;

namespace SecretHitler.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Game/{gameId}/Choose")]
    public class ChooseController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameStateProvider gameStateProvider;

        public ChooseController(IGameRepository gameRepository, IGameStateProvider gameStateProvider)
        {
            _gameRepository = gameRepository;
            this.gameStateProvider = gameStateProvider;
        }

        [HttpPost("{chosenPlayerId}")]
        public IActionResult Choose(int gameId, int chosenPlayerId)
        {
            int playerId;
            if(!int.TryParse(Request.Headers["X-Player"], out playerId))
            {
                throw new Exception("X-Player request header is missing");
            }

            var game = _gameRepository.Get(gameId);
            if(game == null)
            {
                throw new EntityNotFoundException<Game>(gameId);
            }

            if(!game.Players.Any(x => x.Id == chosenPlayerId))
            {
                throw new Exception("Game does not contain chosen player");
            }

            var state = gameStateProvider.Get(game.GameStateId);
            state.Choose(game, playerId, chosenPlayerId);

            return Ok(game);
        }

    }
}