using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.GameStates
{
    public class ChancellorPolicyPickGameState : GameState<ChancellorPolicyPickGameState>
    {
        public override void Choose(Game game, int choosingPlayer, int chosenPlayer)
        {
            base.Choose(game, choosingPlayer, chosenPlayer);
        }

        public override void Vote(Game game, int votingPlayer, bool inFavor)
        {
            base.Vote(game, votingPlayer, inFavor);
        }
        protected override void CheckNextState(Game game)
        {
            base.CheckNextState(game);
        }
    }
}
