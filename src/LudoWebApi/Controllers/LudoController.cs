﻿using System.Collections.Generic;
using System.Linq;
using LudoWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using LudoGameEngine;
using Serilog;

namespace LudoWebApi.Controllers
{
    [Route("api/ludo")]
    [ApiController]
    public class LudoController : ControllerBase
    {
        private ILudoGameContainer ludoGames;
        private IIdGenerator gameIdGenerator;


        public LudoController(ILudoGameContainer lgc, IIdGenerator gid)
        {
            ludoGames = lgc;
            gameIdGenerator = gid;
        }

        // GET: api/Ludo
        /// <summary>
        /// Lista av fia spel 
        /// </summary>
        /// <returns>Lista av fia spel </returns>
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return ludoGames.GetIdsOfAllGames().ToArray();
        }


        // POST: api/Ludo
        /// <summary>
        /// Skåpa ett nytt spel
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public int Post()
        {
            var randomId = gameIdGenerator.GenerateId();
            ludoGames.CreateGame(randomId);
            return randomId;
        }


        // GET: api/Ludo/5
        /// <summary>
        /// Detaljeret information om spelet, som vart alla pjäser finns
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Detaljeret information om spelet, som vart alla pjäser finns</returns>
        [HttpGet("{gameId}")]
        public GameModel GetGame(int gameId)
        {
            var game = ludoGames[gameId];
            int numberOfPlayers = game.GetPlayers().Count();
            var currentPlayer = game.GetCurrentPlayer();
            var players = game.GetAllPiecesInGame();

            return new GameModel() {
                GameId = gameId,
                CurrentPlayerId = currentPlayer == null ? 0 : currentPlayer.PlayerId,
                NumberOfPlayers = numberOfPlayers,
                State = game.GetGameState().ToString(),
                LudoPlayers = ludoGames[gameId].GetPlayers()
            };
        }

        // PUT: api/Ludo/5
        /// <summary>
        /// Ändra placering på en pjäs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{gameId}")]
        public void PutGame(int gameId, [FromBody] MovePiece value)
        {
            var player = ludoGames[gameId].GetPlayers().First(p => p.PlayerId == value.PlayerId);
            ludoGames[gameId].MovePiece(player, value.PieceId, value.NumberOfFields);
        }

        // DELETE: api/Ludo/5
        /// <summary>
        /// Ta bort ett spel
        /// </summary>
        /// <param name="gameId"></param>
        [HttpDelete("{gameId}")]
        public void DeleteGame(int gameId)
        {
            ludoGames.DeleteGame(gameId);
        }

        // GET: api/Ludo/dice
        [HttpGet("dice")]
        public ActionResult<int> RollDice()
        {
            Diece dice = new Diece();
            int value = dice.RollDiece();

            

            return Ok(value);

            
        }
    }
}
