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
        private IRestClient client;
        private static int _diceValue;

        public LudoController(IRestClient _client)
        {
            client = _client;
            client.BaseUrl = new Uri("https://ludolemon-webapi.azurewebsites.net");
        }

        public int RollDice()
        {
            //[Route("giveNumber")]

            var request = new RestRequest("api/ludo/dice", Method.GET);
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

        public IActionResult CreateGame()
        {
            //[Route("createGame")]

            var request = new RestRequest("api/ludo/", Method.POST);
            IRestResponse<int> ludoGameResponse = client.Execute<int>(request);
            var gameId = ludoGameResponse.Data;
            //  Log.Information("Created a game with ID: {gameId}", gameId);


            return View("~/Views/Ludo/GameConfiguration.cshtml", gameId);
        }

       
        public IActionResult GetGameState()
        {
            var request = new RestRequest("/api/Ludo/25396", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = client.Execute(request);
            var gameSetup = ludoGameResponse;
            //PieceModel model = new PieceModel(gameSetup);
            var data = gameSetup.Content;
            var gameInfo = JsonConvert.DeserializeObject<GameModel>(data);

            return View("~/Views/Ludo/Index.cshtml", gameInfo);
        }

        public IActionResult MovePiece(int selectedPiece, int currentPlayerId)
        {
            MovePiece movePiece = new MovePiece();
            movePiece.playerId = currentPlayerId;
            movePiece.pieceId = selectedPiece;
            movePiece.numberOfFields = _diceValue;

            var request = new RestRequest("api/ludo/25396", Method.PUT);
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

            var request = new RestRequest("api/ludo/{gameId}", Method.DELETE);

            return HttpStatusCode(Ok);
        }

        private int HttpStatusCode(Func<OkResult> ok)
        {
            throw new NotImplementedException();
        }

        public IActionResult GameConfiguration(AddPlayer player)
        {
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("/api/Ludo/", Method.GET);
            IRestResponse ludoGameResponse = client.Execute(request);
            var getGames = ludoGameResponse;
            var data = getGames.Content;
            var games = JsonConvert.DeserializeObject<List<int>>(data);
            GameConfiguration game = new GameConfiguration(games);
            ViewBag.Message = player;
            return View("~/Views/Ludo/GameConfiguration.cshtml", game);
        }

        public IActionResult PutGameStateToStart()
        {
            var request = new RestRequest("api/ludo/25396/state", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            IRestResponse addPlayerRequest = client.Put(request);

            return View();
        }

        public IActionResult AddPlayer(string nameOfPlayer, string playerColor)
        {
            AddPlayer playerData = new AddPlayer();

            playerData.Name = nameOfPlayer;
            playerData.Color = playerColor;

            var request = new RestRequest("api/ludo/25396/players", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(playerData);
            IRestResponse addPlayerRequest = client.Post(request);


            return GameConfiguration(playerData);
        }

    }
}