using System;
using System.Collections.Generic;
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
    public class ChooseControllerTest
    {
        private readonly ChooseController sut;

        private readonly IGameStateProvider _gameStateProvider;
        private readonly IGameDataService _gameDataService;

        public ChooseControllerTest()
        {
            _gameStateProvider = Substitute.For<IGameStateProvider>();
            _gameDataService = Substitute.For<IGameDataService>();

            sut = new ChooseController(_gameDataService, _gameStateProvider);
        }

        [Fact]
        public void Choose_WithoutXPlayerHeader_ThrowsBadRequestException() {
            // Arrange
            const int playerId = 12;
            const int gameId = 84;

            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            sut.ControllerContext = controllerContext;

            // Act
            Action action = () => sut.Choose(gameId, playerId);

            // Assert
            action.Should().Throw<BadRequestException>().WithMessage("X-Player request header is missing");
        }

        [Fact]
        public void Choose_WithInvalidPlayerId_ThrowsEntityNotFoundException() {
            // Arrange
            const int playerId = 12;
            const int gameId = 84;
            const int xPlayer = 33;

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Player"] = xPlayer.ToString();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            sut.ControllerContext = controllerContext;

            var game = new Game();
            _gameDataService.GetGame(gameId).Returns(game);

            // Act
            Action action = () => sut.Choose(gameId, playerId);

            // Assert
            action.Should().Throw<EntityNotFoundException<Player>>();
        }

        [Fact]
        public void Choose_WithValidPlayerId_CallsChooseFunctionAndReturnsOk() {
            // Arrange
            const int playerId = 12;
            const int gameId = 84;
            const int xPlayer = 33;

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Player"] = xPlayer.ToString();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            sut.ControllerContext = controllerContext;

            var player = new Player { Id = playerId };
            Game game = new Game
            {
                Players = new List<Player> { player },
                GameStateId = GameState.ChoosePresident
            };
            var gameState = Substitute.For<IGameState>();
            _gameDataService.GetGame(gameId).Returns(game);
            _gameStateProvider.Get(game.GameStateId).Returns(gameState);

            // Act
            sut.Choose(gameId, playerId);
            _gameStateProvider.Received().Get(GameState.ChoosePresident);
            gameState.Received().Choose(game, xPlayer, playerId);

        }
    }
}
