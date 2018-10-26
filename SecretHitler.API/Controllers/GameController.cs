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

namespace SecretHitler.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Game")]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IGameService _gameService;

        public GameController(IGameRepository repository, IPlayerRepository playerRepository, IHubContext<GameHub> hubContext, IGameService gameService)
        {
            _gameRepository = repository;
            _playerRepository = playerRepository;
            _hubContext = hubContext;
            _gameService = gameService;
        }

        /// <summary>
        /// Creates a game
        /// </summary>
        /// <param name="model">A JSON object containing the key "name"</param>
        /// <returns>A JSON object containing the created game</returns>
        [HttpPost]
        public IActionResult CreateGame([FromBody] JToken model)
        {
            var name = model["name"]?.ToString();

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(nameof(name) + " can not be empty");
            }

            var game = new Game
            {
                Name = name,
                GameStateId = GameState.Open,

            };

            return Ok(_gameRepository.Add(game));
        }

        /// <summary>
        /// Starts a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>The started game</returns>
        [HttpPost("Start/{gameId}", Name = "StartGame")]
        public IActionResult StartGame(int gameId)
        {
            try
            {
                var game = _gameService.StartGame(gameId);
                return Ok(game);
            }
            catch(EntityNotFoundException<Game> e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{gameId}", Name = "GetGame")]
        public IActionResult GetGame([FromHeader] int gameId)
        {
            var game = _gameRepository.Get(gameId);
            _hubContext.Clients.All.InvokeAsync("GameInfo", JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Ok(game);
        }

        [HttpGet(Name = "GetGames")]
        public IActionResult GetGames()
        {
            var games = _gameRepository.GetAll().Where(x => x.GameStateId == GameState.Open).ToList();
            return Ok(games);
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
                return BadRequest(nameof(userName) + " can not be empty");
            }

            var game = _gameRepository.GetByJoinKey(joinKey);

            if(game == null)
            {
                return NotFound(joinKey);
            }

            _playerRepository.Add(new Player
            {
                UserName = userName,
                GameId = game.Id
            });
            var player = _playerRepository.GetPlayerByName(userName);
            game = _gameRepository.Get(game.Id);
            _hubContext.Clients.All.InvokeAsync("PlayerJoined", JsonConvert.SerializeObject(game.Players, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

            return Ok(player);
        }

        /// <summary>
        /// Lets a player view the game
        /// </summary>
        /// <param name="joinKey"></param>
        /// <returns>Returns the game</returns>
        [HttpGet("View/{joinKey}")]
        public IActionResult JoinGame(string joinKey)
        {
            var game = _gameRepository.GetByJoinKeyWithPlayers(joinKey);

            if (game == null)
            {
                return NotFound(joinKey);
            }

            return Ok(game);
        }
    }
}