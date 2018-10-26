using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.Services
{
    public interface IGameService
    {
        Game StartGame(int gameId);
        Policy DrawPolicy(int gameId);
    }
}
