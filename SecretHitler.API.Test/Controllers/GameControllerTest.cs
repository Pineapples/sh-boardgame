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

namespace SecretHitler.API.Test.Controllers
{
    public class GameControllerTest
    {
        GameController sut;
        IGameRepository gameRepository;
        IHubContext<GameHub> hubContext;
        IPlayerRepository playerRepository;
        IGameService gameService;

        public GameControllerTest() {
            gameRepository = Substitute.For<IGameRepository>();
            hubContext = Substitute.For<IHubContext<GameHub>>();
            playerRepository = Substitute.For<IPlayerRepository>();
            gameService = Substitute.For<IGameService>();

            sut = new GameController(gameRepository, playerRepository, hubContext, gameService);
        }

        [Fact]
        public void CreateGame_CreatesANewGameWithOpenGamestate() {
            // Arrange
            gameRepository.Add(Arg.Any<Game>()).Returns(parameters => { return parameters[0]; });

            // Act
            var result = sut.CreateGame() as OkObjectResult;
            var resultGame = result.Value as Game;
            // Assert
            gameRepository.Received().Add(Arg.Any<Game>());
            result.Should().NotBeNull();
            resultGame.Should().NotBeNull();
            resultGame.GameStateId.Should().Be(GameState.Open);
        }

        [Fact]
        public void StartGame_WithInvalidGameId_ReturnsBadRequest() {
            // Arrange
            gameService.StartGame(Arg.Any<int>()).Returns(x => { throw new EntityNotFoundException<Game>(); });

            // Act
            var result = sut.StartGame(1) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            result.Value.Should().BeOfType<EntityNotFoundException<Game>>();
        }

        [Fact]
        public void StartGame_WithValidGameId_ShouldReturnOk() {
            // Arrange
            var expectedObject = new Game();
            gameService.StartGame(Arg.Any<int>()).Returns(expectedObject);

            // Act
            var result = sut.StartGame(1) as OkObjectResult;
            var resultGame = result.Value as Game;

            // Assert
            result.Should().NotBeNull();
            resultGame.Should().NotBeNull();
            resultGame.Should().BeEquivalentTo(expectedObject);
        }

        [Fact]
        public void JoinGame_WithExistingJoinKey_ReturnsGame() {
            // Arrange
            var game = new Game();
            gameRepository.GetByJoinKeyWithPlayers(Arg.Any<string>()).Returns(game);

            // Act
            var result = sut.JoinGame("somestring") as OkObjectResult;
            var resultGame = result.Value;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            resultGame.Should().BeEquivalentTo(game);
        }

        [Fact]
        public void JoinGame_WithNonExistingJoinKey_ReturnsNotFound() {
            // Arrange
            const string inputString = "somestring";
            gameRepository.GetByJoinKeyWithPlayers(Arg.Any<string>()).Returns(x => { return null; });

            // Act
            var result = sut.JoinGame(inputString) as NotFoundObjectResult;
            var resultObject = result.Value;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            resultObject.Should().Be(inputString);
        }

        [Fact]
        public void JoinGame_WithEmptyUserName_ReturnsBadRequest() {
            // Arrange
            var jToken = JToken.FromObject(new { name = "" });

            // Act
            var result = sut.JoinGame("key", jToken) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            result.Value.Should().Be("userName can not be empty");
        }

        [Fact]
        public void JoinGame_WithValidUserNameAndInvalidJoinKey_ReturnsNotFound() {
            // Arrange
            const string joinKey = "somestring";
            var jToken = JToken.FromObject(new { userName = "Klaas" });
            gameRepository.GetByJoinKey(joinKey).Returns(x => { return null; });

            // Act
            var result = sut.JoinGame(joinKey, jToken) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            result.Value.Should().Be(joinKey);
        }

        [Fact]
        public void JoinGameWithValidUserNameAndValidJoinKey_JoinsGameAndBroadcasts() {
            // Arrange
            const string joinKey = "somestring";
            const string userName = "Klaas";
            const int gameId = 12;

            var player = new Player{ UserName = userName, GameId = gameId };
            var game = new Game();
            game.Id = gameId;
            var jToken = JToken.FromObject(new { userName });
            gameRepository.GetByJoinKey(joinKey).Returns(game);
            gameRepository.Get(gameId).Returns(game);
            playerRepository.GetPlayerByName(userName).Returns(player);

            // Act
            var result = sut.JoinGame(joinKey, jToken) as OkObjectResult;

            // Assert
            gameRepository.Received().GetByJoinKey(joinKey);
            gameRepository.Received().Get(gameId);
            playerRepository.Received().Add(Arg.Is<Player>(x => x.UserName == userName && x.GameId == gameId));
            playerRepository.Received().GetPlayerByName(userName);
            hubContext.Received().Clients.All.SendAsync("PlayerJoined", Arg.Any<string>());
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().Be(player);
        }
    }
}
