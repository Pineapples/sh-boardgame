using Microsoft.AspNetCore.Mvc;
using SecretHitler.API.GameStates;
using SecretHitler.API.Repositories;
using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using SecretHitler.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Game/{gameId}/Vote")]
    public class VoteController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameStateProvider gameStateProvider;

        public VoteController(IGameRepository gameRepository, IGameStateProvider gameStateProvider)
        {
            _gameRepository = gameRepository;
            this.gameStateProvider = gameStateProvider;
        }

        [HttpPost("{inFavor}")]
        public IActionResult Vote(int gameId, bool inFavor)
        {
            int playerId;
            if (!int.TryParse(Request.Headers["X-Player"], out playerId))
            {
                throw new Exception("X-Player request header is missing");
            }

            var game = _gameRepository.Get(gameId);
            if (game == null)
            {
                throw new EntityNotFoundException<Game>(gameId);
            }

            var state = gameStateProvider.Get(game.GameStateId);
            state.Vote(game, playerId, inFavor);

            return Ok(game);
        }
    }
}
