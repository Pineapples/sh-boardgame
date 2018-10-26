using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.GameStates
{
    public abstract class GameState<T> : IGameState, IStateObject<T>
    {
        public virtual void Vote(Game game, int votingPlayer, bool inFavor)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Choose(Game game, int choosingPlayer, int chosenPlayer)
        {
            throw new NotImplementedException();
        }

        protected virtual void CheckNextState(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
