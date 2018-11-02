using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using SecretHitler.API.Controllers;
using SecretHitler.API.Hubs;
using SecretHitler.API.Repositories;
using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using Xunit;
using FluentAssertions;
using SecretHitler.Models.Exceptions;
using Newtonsoft.Json.Linq;
using SecretHitler.API.DataServices.Interface;

namespace SecretHitler.API.Test.Controllers
{
    public class GameControllerTest
    {
        GameController sut;
        IGameDataService _gameDataService;
        IHubContext<GameHub> _hubContext;
        IPlayerRepository _playerRepository;
        IGameService _gameService;

        public GameControllerTest() {
            _gameDataService = Substitute.For<IGameDataService>();
            _hubContext = Substitute.For<IHubContext<GameHub>>();
            _playerRepository = Substitute.For<IPlayerRepository>();
            _gameService = Substitute.For<IGameService>();

            sut = new GameController(_gameDataService, _playerRepository, _hubContext, _gameService);
        }

        [Fact]
        public void CreateGame_CreatesANewGameWithOpenGamestate() {
            // Act
            var result = sut.CreateGame() as OkObjectResult;

            // Assert
            _gameService.Received().CreateGame();
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void StartGame_WithValidGameId_ShouldReturnOk() {
            // Arrange
            const int gameId = 12;
            var expectedObject = new Game();
            _gameService.StartGame(Arg.Any<int>()).Returns(expectedObject);

            // Act
            var result = sut.StartGame(gameId) as OkObjectResult;
            var resultGame = result.Value as Game;

            // Assert
            _gameService.Received().StartGame(gameId);
            result.Should().NotBeNull();
            resultGame.Should().NotBeNull();
            resultGame.Should().BeEquivalentTo(expectedObject);
        }

        [Fact]
        public void JoinGame_WithExistingJoinKey_ReturnsGame()
        {
            // Arrange
            var game = new Game();
            _gameService.ViewGame(Arg.Any<string>()).Returns(game);

            // Act
            var result = sut.JoinGame("somestring") as OkObjectResult;
            var resultGame = result.Value;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            resultGame.Should().BeEquivalentTo(game);
        }

        [Fact]
        public void JoinGame_WithEmptyUserName_ReturnsBadRequest() {
            // Arrange
            var jToken = JToken.FromObject(new { name = "" });

            // Act
            Action action = () => sut.JoinGame("key", jToken);

            // Assert
            action.Should().Throw<BadRequestException>()
                  .WithMessage("userName can not be empty");
        }

        [Fact]
        public void JoinGameWithValidUserNameAndValidJoinKey_JoinsGameAndBroadcasts() {
            // Arrange
            const string joinKey = "somestring";
            const string userName = "Klaas";
            const int gameId = 12;

            var player = new Player{ UserName = userName, GameId = gameId };
            var jToken = JToken.FromObject(new { userName });
            _gameService.JoinGame(joinKey, userName).Returns(player);

            // Act
            var result = sut.JoinGame(joinKey, jToken) as OkObjectResult;

            // Assert
            _gameService.Received().JoinGame(joinKey, userName);
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().Be(player);
        }
    }
}
