using System;
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

        public IActionResult CreateGame()
        {
            //[Route("createGame")]

            var request = new RestRequest("api/ludo/", Method.POST);
            IRestResponse<int> ludoGameResponse = _client.Execute<int>(request);
            var gameId = ludoGameResponse.Data;
            //  Log.Information("Created a game with ID: {gameId}", gameId);

            return GameConfiguration(gameId);
        }

        public IActionResult GetTotalGameInfo(int gameID)
        {
            var request = new RestRequest($"/api/Ludo/{gameID}", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = _client.Execute(request);
            var gameSetup = ludoGameResponse;
            var data = gameSetup.Content;
            var gameInfo = JsonConvert.DeserializeObject<GameModel>(data);

            return View("~/Views/Ludo/Index.cshtml", gameInfo);
        }


        public IActionResult MovePiece(int selectedPiece, int currentPlayerId, int gameID)
        {
            MovePiece movePiece = new MovePiece
            {
                playerId = currentPlayerId,
                pieceId = selectedPiece,
                numberOfFields = _diceValue
            };

            var request = new RestRequest($"api/ludo/{gameID}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(movePiece);
            IRestResponse ludoGameResponse = _client.Put(request);

            return GetTotalGameInfo(gameID);
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

        public IActionResult GameConfiguration(int gameID)
        {
            GameConfiguration gameConfig = new GameConfiguration(gameID);

            return View("~/Views/Ludo/GameConfiguration.cshtml", gameConfig);
        }

        public IActionResult PutGameStateToStart(int gameID)
        {
            var request = new RestRequest($"api/ludo/{gameID}/state", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            IRestResponse addPlayerRequest = _client.Put(request);
            return GetTotalGameInfo(gameID);
        }

        public IActionResult AddPlayer(string nameOfPlayer, string playerColor, int gameID)
        {
            AddPlayer playerData = new AddPlayer();

            playerData.Name = nameOfPlayer;
            playerData.Color = playerColor;

            var request = new RestRequest($"api/ludo/{gameID}/players", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(playerData);
            IRestResponse addPlayerRequest = _client.Post(request);

            return GameConfiguration(gameID);
        }

    }
}