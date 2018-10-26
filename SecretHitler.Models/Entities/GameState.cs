using System;
using System.Collections.Generic;
using System.Text;

namespace SecretHitler.Models.Entities
{
    public enum GameState
    {
        Error = 0,
        None = 1,
        Open = 2,
        ChoosePresident = 3,
        ChooseChancellor = 4,
        VoteForGovernment = 5,
        PresidentPolicyPick = 6,
        ChancellorPolicyPick = 7
    }
}
