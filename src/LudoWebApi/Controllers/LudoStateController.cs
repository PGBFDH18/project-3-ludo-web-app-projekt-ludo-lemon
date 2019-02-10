using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudoWebApi.Controllers
{
    [Route("api/ludo/{gameId}/state")]
    [ApiController]
    public class LudoStateController : ControllerBase
    {
        private ILudoGameContainer ludoGames;

        public LudoStateController(ILudoGameContainer lgc)
        {
            ludoGames = lgc;
        }

        // GET: api/ludo/3/state
        /// <summary>
        /// Get the state of the current game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        public string Get(int gameId)
        {
            return ludoGames[gameId].GetGameState().ToString();
        }

        // PUT: api/LudoState/5
        /// <summary>
        /// If possible change the state to started
        /// </summary>
        /// <param name="gameId"></param>
        [HttpPut]
        public ActionResult Put(int gameId)
        {
            try
            {
                ludoGames[gameId].StartGame();
            }
            catch
            {
                return BadRequest("Uable to start game");
            }
            return Ok();
        }
    }
}
