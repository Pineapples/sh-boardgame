using System;
using NSubstitute;
using SecretHitler.API.Controllers;
using SecretHitler.API.GameStates;
using SecretHitler.API.Repositories;
using Xunit;

namespace SecretHitler.API.Test.Controllers
{
    public class ChooseControllerTest
    {
        ChooseController sut;

        IGameStateProvider _gameStateProvider;
        IGameRepository _gameRepository;

        public ChooseControllerTest()
        {
            _gameStateProvider = Substitute.For<IGameStateProvider>();
            _gameRepository = Substitute.For<IGameRepository>();

            sut = new ChooseController(_gameRepository, _gameStateProvider);
        }

        [Fact]
        public void Choose_WithInvalidPlayerId_ThrowsException() {
            // Arrange
            const int playerId = 12;
            const int gameId = 84;

            // Act
            var result = sut.Choose(gameId, playerId) as ;

            // Assert
        }
    }
}
