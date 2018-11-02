using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SecretHitler.API.Hubs;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SecretHitler.API.Services;
using SecretHitler.Models.Exceptions;
using System;
using SecretHitler.API.DataServices.Interface;

namespace SecretHitler.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Game")]
    public class GameController : Controller
    {
        private readonly IGameDataService _gameDataService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IGameService _gameService;

        public GameController(IGameDataService gameDataService, IPlayerRepository playerRepository, IHubContext<GameHub> hubContext, IGameService gameService)
        {
            this._gameDataService = gameDataService;
            this._playerRepository = playerRepository;
            this._hubContext = hubContext;
            this._gameService = gameService;
        }

        /// <summary>
        /// Creates a game
        /// </summary>
        /// <returns>A JSON object containing the created game</returns>
        [HttpPost]
        public IActionResult CreateGame()
        {
            var game = new Game
            {
                GameStateId = GameState.Open,

            };
            return Ok(_gameDataService.AddGame(game));
        }

        /// <summary>
        /// Starts a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>The started game</returns>
        [HttpPost("Start/{gameId}", Name = "StartGame")]
        public IActionResult StartGame(int gameId)
        {
            var game = _gameService.StartGame(gameId);
            return Ok(game);
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <returns>The game.</returns>
        /// <param name="gameId">Game identifier.</param>
        [HttpGet("{gameId}", Name = "GetGame")]
        public IActionResult GetGame([FromHeader] int gameId)
        {
            var game = _gameDataService.GetGame(gameId);
            _hubContext.Clients.All.SendAsync("GameInfo", JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Ok(game);
        }

        /// <summary>
        /// Gets all open games
        /// </summary>
        /// <returns>A list of open games</returns>
        [HttpGet(Name = "GetGames")]
        public IActionResult GetGames()
        {
            return Ok(_gameDataService.GetOpenGames());
        }


        /// <summary>
        /// Lets a player join a game
        /// </summary>
        /// <param name="joinKey">The joinkey of the game the player wants to join</param>
        /// <param name="model">A json object containing the key "userName"</param>
        /// <returns>The player that joined</returns>
        [HttpPost("Join/{joinKey}")]
        public IActionResult JoinGame(string joinKey, [FromBody] JToken model)
        {
            var userName = model["userName"]?.ToString();

            if (string.IsNullOrEmpty(userName))
            {
                throw new BadRequestException(nameof(userName) + " can not be empty");
            }

            //game = _gameRepository.Get(game.Id);
            //_hubContext.Clients.All.SendAsync("PlayerJoined", JsonConvert.SerializeObject(game.Players, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

            return Ok(_gameService.JoinGame(joinKey, userName));
        }

        /// <summary>
        /// Lets a player view the game
        /// </summary>
        /// <param name="joinKey"></param>
        /// <returns>Returns the game</returns>
        [HttpGet("View/{joinKey}")]
        public IActionResult JoinGame(string joinKey)
        {
            return Ok(_gameService.ViewGame(joinKey));
        }
    }
}