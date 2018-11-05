using System;
using FluentAssertions;
using NSubstitute;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.Repositories;
using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using Xunit;

namespace SecretHitler.API.Test.Services
{
    public class GameServiceTest
    {
        private readonly GameService sut;
        private readonly IPolicyRepository _policyRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameDataService _gameDataService;

        public GameServiceTest()
        {
            _policyRepository = Substitute.For<IPolicyRepository>();
            _playerRepository = Substitute.For<IPlayerRepository>();
            _gameDataService = Substitute.For<IGameDataService>();

            sut = new GameService(_policyRepository, _playerRepository, _gameDataService);
        }

        [Fact]
        public void CreateGame_CreatesANewGameWithOpenGamestate()
        {
            // Arrange
            _gameDataService.AddGame(Arg.Any<Game>()).Returns(parameters => { return parameters[0]; });

            // Act
            var result = sut.CreateGame();

            // Assert
            _gameDataService.Received().AddGame(Arg.Any<Game>());
            result.GameStateId.Should().Be(GameState.Open);
        }

        //[Fact]
        //public void StartGame_WithInvalidGameId_ReturnsBadRequest()
        //{
        //    // Arrange
        //    _gameService.StartGame(Arg.Any<int>()).Returns(x => { throw new EntityNotFoundException<Game>(); });

        //    // Act
        //    var result = sut.StartGame(1) as BadRequestObjectResult;

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<BadRequestObjectResult>();
        //    result.Value.Should().BeOfType<EntityNotFoundException<Game>>();
        //}

        //[Fact]
        //public void StartGame_WithValidGameId_ShouldReturnOk()
        //{
        //    // Arrange
        //    const int gameId = 12;
        //    var expectedObject = new Game();
        //    _gameDataService.GetGame(gameId).Returns(expectedObject);

        //    // Act
        //    var result = sut.StartGame(gameId);

        //    // Assert
        //    result.Should().NotBeNull();
        //    resultGame.Should().NotBeNull();
        //    resultGame.Should().BeEquivalentTo(expectedObject);
        //}

        [Fact]
        public void ViewGame_WithExistingJoinKey_ReturnsGame()
        {
            // Arrange
            const string joinKey = "asdfasdf";
            var game = new Game();
            _gameDataService.GetGameWithPlayers(joinKey).Returns(game);

            // Act
            var result = sut.ViewGame(joinKey);

            // Assert
            _gameDataService.Received().GetGameWithPlayers(joinKey);
            result.Should().Be(game);
        }

        [Fact]
        public void JoinGameWithValidUserNameAndValidJoinKey_JoinsGameAndBroadcasts()
        {
            // Arrange
            const string joinKey = "somestring";
            const string userName = "Klaas";
            const int gameId = 12;

            var player = new Player { UserName = userName, GameId = gameId };
            var game = new Game();
            game.Id = gameId;
            _gameDataService.GetGame(joinKey).Returns(game);
            _playerRepository.GetPlayerByName(userName, gameId).Returns(player);

            // Act
            var result = sut.JoinGame(joinKey, userName);

            // Assert
            _gameDataService.Received().GetGame(joinKey);
            _playerRepository.Received().Add(Arg.Is<Player>(x => x.UserName == userName && x.GameId == gameId));
            _playerRepository.Received().GetPlayerByName(userName, gameId);
            result.Should().Be(player);
        }
    }
}
