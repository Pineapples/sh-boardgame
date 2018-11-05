using Microsoft.AspNetCore.Mvc;
using SecretHitler.API.DataServices.Interface;
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
        private readonly IGameDataService _gameDataService;
        private readonly IGameStateProvider _gameStateProvider;

        public VoteController(IGameDataService gameDataService, IGameStateProvider gameStateProvider)
        {
            this._gameDataService = gameDataService;
            this._gameStateProvider = gameStateProvider;
        }

        [HttpPost("{inFavor}")]
        public IActionResult Vote(int gameId, bool inFavor)
        {
            if (!int.TryParse(Request.Headers["X-Player"], out var playerId))
            {
                throw new BadRequestException("X-Player request header is missing");
            }

            var game = _gameDataService.GetGame(gameId);
            var state = _gameStateProvider.Get(game.GameStateId);
            state.Vote(game, playerId, inFavor);
            return Ok(game);
        }
    }
}
