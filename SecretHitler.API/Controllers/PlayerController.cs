using Microsoft.AspNetCore.Mvc;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretHitler.API.Controllers
{
    [Route("api/Player")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] string userName)
        //{
        //    var player = _playerRepository.GetPlayerByName(userName);
        //    if(player == null)
        //    {
        //        _playerRepository.Add(new Player
        //        {
        //            UserName = userName
        //        });
        //        player = _playerRepository.GetPlayerByName(userName);
        //    }            
        //    return Ok(player);
        //}
    }
}
