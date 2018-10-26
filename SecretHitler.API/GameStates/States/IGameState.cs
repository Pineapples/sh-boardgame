using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.GameStates
{
    public interface IGameState
    {
        void Choose(Game game, int choosingPlayer, int chosenPlayer);
        void Vote(Game game, int votingPlayer, bool inFavor);
    }
}
