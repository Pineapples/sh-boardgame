using System;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SecretHitler.API.Controllers;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.GameStates;
using SecretHitler.Models.Entities;
using SecretHitler.Models.Exceptions;
using Xunit;

namespace SecretHitler.API.Test.Controllers
{
    public class VoteControllerTest
    {
        private readonly VoteController sut;

        private readonly IGameDataService _gameDataService;
        private readonly IGameStateProvider _gameStateProvider;

        public VoteControllerTest()
        {
            _gameDataService = Substitute.For<IGameDataService>();
            _gameStateProvider = Substitute.For<IGameStateProvider>();

            sut = new VoteController(_gameDataService, _gameStateProvider);
        }

        [Fact]
        public void Vote_WithoutXPlayerHeader_ThrowsBadRequestException()
        {
            // Arrange
            const int gameId = 84;

            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            sut.ControllerContext = controllerContext;

            // Act
            Action action = () => sut.Vote(gameId, true);

            // Assert
            action.Should().Throw<BadRequestException>().WithMessage("X-Player request header is missing");
        }

        [Fact]
        public void Vote_WithValidParameters_CallsStateVote() 
        {
            // Arrange
            const int gameId = 84;
            const int xPlayerId = 12;
            const bool inFavor = true;

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Player"] = xPlayerId.ToString();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            sut.ControllerContext = controllerContext;

            var game = new Game
            {
                GameStateId = GameState.VoteForGovernment
            };

            var gameState = Substitute.For<IGameState>();
            _gameStateProvider.Get(Arg.Any<GameState>()).Returns(gameState);
            _gameDataService.GetGame(gameId).Returns(game);

            // Act
            var result = sut.Vote(gameId, inFavor) as OkObjectResult;

            // Assert
            _gameStateProvider.Received().Get(GameState.VoteForGovernment);
            _gameDataService.Received().GetGame(gameId);
            gameState.Received().Vote(game, xPlayerId, inFavor);
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().Be(game);
        }
    }
}
