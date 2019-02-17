using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemon_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
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


        public int RollDice(string nameOfPlayer)
        {
            var request = new RestRequest("api/ludo/dice", Method.GET);
            IRestResponse<int> ludoGameResponse = _client.Execute<int>(request);

            var diceValue = ludoGameResponse.Data;
            _diceValue = diceValue;
            
            Log.Information("Player: {nameOfPlayer} rolled the dice. The dice roll was: {diceValue}", nameOfPlayer, diceValue);

            return diceValue;
        }

        public IActionResult Welcome()
        {
            Log.Information($"A new client connected at IP{HttpContext.Connection.RemoteIpAddress.ToString()}");
            return View();
        }

        public IActionResult CreateGame()
        {
            var request = new RestRequest("api/ludo/", Method.POST);
            IRestResponse<int> ludoGameResponse = _client.Execute<int>(request);
            var gameId = ludoGameResponse.Data;
            //  Log.Information("Created a game with ID: {gameId}", gameId);

            var gameModel = GetTotalGameInfo(gameId);
            Log.Information("Created game with gameId: {gameId}", gameId);

            return View("~/Views/Ludo/GameConfiguration.cshtml", gameModel);
        }

        public GameModel GetTotalGameInfo(int gameId)
        {
            var request = new RestRequest($"/api/Ludo/{gameId}", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = _client.Execute(request);
            var gameSetup = ludoGameResponse;
            var data = gameSetup.Content;
            var gameModel = JsonConvert.DeserializeObject<GameModel>(data);
            Log.Information("Fetching total info about gameId: {gameId}", gameId);
            return gameModel;
        }

        [HttpGet("joingame")]
        public IActionResult JoinGame()
        {
            var request = new RestRequest("api/ludo", Method.GET);
            IRestResponse ludoListResponse = _client.Execute(request);
            GameList data = new GameList()
            {
                ListOfCreatedGames = JsonConvert.DeserializeObject<int[]>(ludoListResponse.Content)
            };
            Log.Information("Listing available games: {data}", data);
            return View(data);
        }

        public IActionResult MovePiece(int selectedPiece, int currentPlayerId, int gameId, string nameOfCurrentPlayer)
        {
            MovePiece movePiece = new MovePiece
            {
                playerId = currentPlayerId,
                pieceId = selectedPiece,
                numberOfFields = _diceValue
            };

            var request = new RestRequest($"api/ludo/{gameId}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(movePiece);
            IRestResponse ludoGameResponse = _client.Put(request);

            var gameModel = GetTotalGameInfo(gameId);
            Log.Information("Player: {nameOfCurrentPlayer} moved {_diceValue} steps with pieceId: {selectedPiece}", nameOfCurrentPlayer, _diceValue, selectedPiece);
            return View("~/Views/Ludo/Index.cshtml", gameModel);
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
        
        public IActionResult GameBoardSavedGame(int gameId)
        {
            var gameModel = GetTotalGameInfo(gameId);
            Log.Information("Loading game board for saved games with gameId: {gameId}", gameId);
            return View("~/Views/Ludo/Index.cshtml", gameModel);
        }

        public IActionResult GameBoardNewGame(int gameId)
        {
            var request = new RestRequest($"api/ludo/{gameId}/state", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            IRestResponse addPlayerRequest = _client.Put(request);
            var gameModel = GetTotalGameInfo(gameId);
            Log.Information("Loading game board for new game with gameId: {gameId}", gameId);
            return View("~/Views/Ludo/Index.cshtml", gameModel);
        }

        public IActionResult AddPlayer(string nameOfPlayer, string playerColor, int gameId)
        {
            AddPlayer playerData = new AddPlayer();

            playerData.Name = nameOfPlayer;
            playerData.Color = playerColor;

            var request = new RestRequest($"api/ludo/{gameId}/players", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(playerData);
            IRestResponse addPlayerRequest = _client.Post(request);

            var gameModel = GetTotalGameInfo(gameId);
            Log.Information("Added player with name: {nameOfPlayer}" + " with color: {playerColor}" + " and gameId: {gameId}", nameOfPlayer, playerColor, gameId);
            return View("~/Views/Ludo/GameConfiguration.cshtml", gameModel);
        }
    }
}