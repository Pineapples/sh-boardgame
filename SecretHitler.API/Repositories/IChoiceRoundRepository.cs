using System;
using SecretHitler.Models.Entities;

namespace SecretHitler.API.Repositories
{
    public interface IChoiceRoundRepository
    {
        ChoiceRound GetLatestChoiceRoundByGameId(int gameId);
    }
}
