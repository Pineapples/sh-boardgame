using System;
using System.Linq;
using AutoMapper;
using SecretHitler.API.DataServices.Interface;
using SecretHitler.API.Hubs;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Dto;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.GameStates
{
    public class ChoosePresidentGameState : GameState<ChoosePresidentGameState>
    {
        private readonly IChoiceRoundRepository _choiceRoundRepository;
        private readonly IChoiceRepository _choiceRepository;
        private readonly IGameDataService _gameDataService;
        private readonly IGameHub _gameHub;

        public ChoosePresidentGameState(IChoiceRoundRepository choiceRoundRepository, 
                                        IChoiceRepository choiceRepository,
                                        IGameDataService gameDataService,
                                        IGameHub gameHub)
        {
            this._choiceRoundRepository = choiceRoundRepository;
            this._choiceRepository = choiceRepository;
            this._gameDataService = gameDataService;
            this._gameHub = gameHub;
        }

        public override void Choose(Game game, int choosingPlayer, int chosenPlayer)
        {
            var choiceRound = this._choiceRoundRepository.GetLatestChoiceRoundByGameId(game.Id);
            var currentChoice = choiceRound.Choices.FirstOrDefault(x => x.ChooserId == choosingPlayer);
            if(currentChoice != null) {
                currentChoice.ChosenId = chosenPlayer;
                this._choiceRepository.Update(currentChoice);
            } else {
                currentChoice = new Choice
                {
                    ChoiceRoundId = choiceRound.Id,
                    ChooserId = choosingPlayer,
                    ChosenId = chosenPlayer
                };
                this._choiceRepository.Add(currentChoice);
            }
            this._gameHub.SendToGroup(WebSocketMessages.PLAYER_CHOSE, Mapper.Map<ChoiceDto>(currentChoice), choiceRound.GameId);
            CheckNextState(game);
        }

        protected override void CheckNextState(Game game)
        {
            var choiceRound = this._choiceRoundRepository.GetLatestChoiceRoundByGameId(game.Id);
            if (choiceRound.Choices.Count == game.Players.Count) {
                game.GameStateId = GameState.ChooseChancellor;
                game.ChoiceRounds.Add(new ChoiceRound());
                this._gameDataService.UpdateGame(game);
                this._gameHub.SendToGroup(WebSocketMessages.NEXT_STATE, Mapper.Map<GameStatusDto>(game), game.Id);
            }
        }
    }
}
