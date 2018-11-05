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
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.Extensions;

namespace SecretHitler.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Game/{gameId}/Choose")]
    public class ChooseController : Controller
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameStateProvider _gameStateProvider;

        public ChooseController(IGameDataService gameDataService, IGameStateProvider gameStateProvider)
        {
            this._gameDataService = gameDataService;
            this._gameStateProvider = gameStateProvider;
        }

        [HttpPost("{chosenPlayerId}")]
        public IActionResult Choose(int gameId, int chosenPlayerId)
        {
            if(!int.TryParse(Request.Headers["X-Player"], out var playerId))
            {
                throw new BadRequestException("X-Player request header is missing");
            }

            var game = _gameDataService.GetGame(gameId);
            if(!game.HasPlayer(chosenPlayerId))
            {
                throw new EntityNotFoundException<Player>(chosenPlayerId);
            }

            var state = _gameStateProvider.Get(game.GameStateId);
            state.Choose(game, playerId, chosenPlayerId);
            return Ok(game);
        }

    }
}