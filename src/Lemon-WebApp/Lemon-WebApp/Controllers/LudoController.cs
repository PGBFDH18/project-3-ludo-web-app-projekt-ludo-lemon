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
        private IRestClient _client;
        private static int _diceValue;

        public LudoController(IRestClient client)
        {
            _client = client;
            _client.BaseUrl = new Uri("https://ludolemon-webapi.azurewebsites.net");
        }

        public int RollDice()
        {
            //[Route("giveNumber")]

            var request = new RestRequest("api/ludo/dice", Method.GET);
            IRestResponse<int> ludoGameResponse = _client.Execute<int>(request);

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

        public void CreateGame()
        {
            //[Route("createGame")]

            var request = new RestRequest("api/ludo/", Method.POST);
            IRestResponse<int> ludoGameResponse = _client.Execute<int>(request);
            var gameId = ludoGameResponse.Data;
            Log.Information("Created a game with ID: {gameId}", gameId);
        }

        public IActionResult GetGameState(int gameId = 41742) // <- Default-value to avoid ArgumentOutOfRangeException when not argument is passed to the method.
        {
            //[Route("getGameInformation")]

            //var request = new RestRequest("/api/Ludo/41742", Method.GET);
            var request = new RestRequest($"/api/Ludo/{gameId}", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = _client.Execute(request);
            var gameSetup = ludoGameResponse;
            //PieceModel model = new PieceModel(gameSetup);
            var data = gameSetup.Content;
            var gameInfo = JsonConvert.DeserializeObject<GameModel>(data);

            return View("~/Views/Ludo/Index.cshtml", gameInfo);
        }
        
        public IActionResult MovePiece(string pieceId, int currentPlayerId, int gameId)
        {
            MovePiece movePiece = new MovePiece
            {
                playerId = currentPlayerId,
                pieceId = int.Parse(pieceId),
                numberOfFields = _diceValue
            };

            //var request = new RestRequest("api/ludo/41742", Method.PUT);
            var request = new RestRequest($"api/ludo/{gameId}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(movePiece);
            IRestResponse ludoGameResponse = _client.Put(request);

            return GetGameState(gameId);
            
            //var request = new RestRequest("/api/Ludo/115", Method.PUT);
            ////request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            

        }


        public int DeleteGame()  // (vi ska testa den när vi skapar spel)
        {
            //[Route("deleteGame")]

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

            var request = new RestRequest("/api/Ludo/115", Method.GET);
            IRestResponse ludoGameResponse = _client.Execute(request);
            var gameSetup = ludoGameResponse;
            var  data = gameSetup.Content;
            var gameInfo = JsonConvert.DeserializeObject<GameModel>(data);
            return View();
        }

        public IActionResult GameConfiguration()
        {
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("/api/Ludo/", Method.GET);
            IRestResponse ludoGameResponse = client.Execute(request);
            var getGames = ludoGameResponse;
            var data = getGames.Content;
            var games = JsonConvert.DeserializeObject<List<int>>(data);
            GameConfiguration game = new GameConfiguration(games);
            return View(game);
        }

        /*public IActionResult AddPlayer()
        {
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");
            var request = new RestRequest("api/ludo/41742", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(movePiece);
            IRestResponse ludoGameResponse = client.Put(request);
        }
        */
    }
}