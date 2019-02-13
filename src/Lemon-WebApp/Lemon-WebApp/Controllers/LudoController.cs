﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemon_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Serilog;


namespace Lemon_WebApp.Controllers
{
    public class LudoController : Controller
    {
        private static int _diceValue;

        public int RollDice()
        {
            //[Route("giveNumber")]
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("api/ludo/dice", Method.GET);
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource
            IRestResponse<int> ludoGameResponse = client.Execute<int>(request);

            var diceValue = ludoGameResponse.Data;
            _diceValue = diceValue;

            //DiceModel model = new DiceModel(diceValue);
            Log.Information("The dice roll was: {diceValue}", diceValue);

            return diceValue;
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public void Something()
        {
            //[Route("createGame")]
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("api/ludo/", Method.POST);
            IRestResponse<int> ludoGameResponse = client.Execute<int>(request);
            var gameId = ludoGameResponse.Data;
            Log.Information("Created a game with ID: {gameId}", gameId);
            
        }

        public IActionResult GetGameState()
        {
            //[Route("getGameInformation")]
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("/api/Ludo/41742", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = client.Execute(request);
            var gameSetup = ludoGameResponse;
            //PieceModel model = new PieceModel(gameSetup);
            var data = gameSetup.Content;
            var gameInfo = JsonConvert.DeserializeObject<GameModel>(data);

            return View("~/Views/Ludo/Index.cshtml", gameInfo);
        }
        
        public IActionResult MovePiece(string pieceId, int currentPlayerId)
        {
            MovePiece movePiece = new MovePiece();

            movePiece.playerId = currentPlayerId;
            movePiece.pieceId = int.Parse(pieceId);
            movePiece.numberOfFields = _diceValue;
           
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");
            var request = new RestRequest("api/ludo/41742", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(movePiece);
            IRestResponse ludoGameResponse = client.Put(request);

            return GetGameState();
            
            //var request = new RestRequest("/api/Ludo/115", Method.PUT);
            ////request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            

        }


        public int DeleteGame()  // (vi ska testa den när vi skapar spel)
        {
            //[Route("deleteGame")]
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("api/ludo/{gameId}", Method.DELETE);

            return HttpStatusCode(Ok);
        }

        private int HttpStatusCode(Func<OkResult> ok)
        {
            throw new NotImplementedException();
        }

        public IActionResult tryToFindAllPiecePositions()
        {
            //[Route("getGameInformation")]
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("/api/Ludo/115", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = client.Execute(request);
            var gameSetup = ludoGameResponse;
            //PieceModel model = new PieceModel(gameSetup);
            var  data = gameSetup.Content;
            var gameInfo = JsonConvert.DeserializeObject<GameModel>(data);
            return View();
        }
    }
}