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
            var game = GetTotalGameInfo(gameId);
            var model = new CreateGameModel();
            model.numberOfPlayers = game.numberOfPlayers;
            model.ludoPlayers = game.ludoPlayers ?? new List<LudoPlayer>();
            model.gameId = game.gameId;


            //var gameModel = GetTotalGameInfo(gameId);
            Log.Information("Created game with gameId: {gameId}", gameId);

            return View("~/Views/Ludo/GameConfiguration.cshtml", model);
        }

        protected GameModel GetTotalGameInfo(int gameId)
        {
            var request = new RestRequest($"/api/Ludo/{gameId}", Method.GET);
            IRestResponse ludoGameResponse = _client.Execute(request);
            var gameSetup = ludoGameResponse;
            var data = gameSetup.Content;
            var gameModel = JsonConvert.DeserializeObject<GameModel>(data);
            Log.Information("Fetching total info about gameId: {gameId}", gameId);
            return gameModel;
        }

        public GameList GetGames()
        {
            var request = new RestRequest("api/ludo", Method.GET);
            IRestResponse ludoListResponse = _client.Execute(request);
            GameList data = new GameList()
            {
                ListOfCreatedGames = JsonConvert.DeserializeObject<int[]>(ludoListResponse.Content)
            };
            Log.Information("Listing available games: {data}", data);
            return data;
        }

        [HttpGet("joingame")]
        public IActionResult JoinGame()
        {
            var listOfCreatedGames = GetGames();
            return View(listOfCreatedGames);
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
            IRestResponse backendResponse = _client.Put(request);

            var gameModel = GetTotalGameInfo(gameId);

            // Try get winner name and assign it to gameModel so it can be accessed in Index.cshtml.
            request = new RestRequest($"api/ludo/{gameId}/winner", Method.GET);
            backendResponse = _client.Get(request);
            string winner = JsonConvert.DeserializeObject<string>(backendResponse.Content);
            gameModel.winner = winner == "N/A" ? null : winner;

            Log.Information("Player: {nameOfCurrentPlayer} moved {_diceValue} steps with pieceId: {selectedPiece}", nameOfCurrentPlayer, _diceValue, selectedPiece);
            return View("~/Views/Ludo/Index.cshtml", gameModel);
        }

        public IActionResult DeleteGame(int gameId)
        {
            var request = new RestRequest($"api/ludo/{gameId}", Method.DELETE);
            _client.Delete(request);
            Log.Information("Game: {gameId} was deleted", gameId);

            var listOfCreatedGames = GetGames();
            return View("~/Views/Ludo/JoinGame.cshtml", listOfCreatedGames);
        }


        public IActionResult GameHandler(int gameId, string joinGame)
        {
            if (joinGame != null)
            {
                var gameModel = GetTotalGameInfo(gameId);
                Log.Information("Loading game board for saved games with gameId: {gameId}", gameId);
                return View("~/Views/Ludo/Index.cshtml", gameModel);
            }
            return DeleteGame(gameId);
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

        [HttpPost]
        public IActionResult AddPlayer(CreateGameModel model)
        {
            var gameId = model.gameId;
            var game = GetTotalGameInfo(gameId);
            CreateGameModel postBackModel = new CreateGameModel();
            if (ModelState.IsValid)
            {
                
                AddPlayer playerData = new AddPlayer();
              
                playerData.Name = model.ludoplayer.name;
                playerData.Color = model.ludoplayer.playerColor;

                var request = new RestRequest($"api/ludo/{gameId}/players", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(playerData);
                IRestResponse addPlayerRequest = _client.Post(request);
                var  postedgame = GetTotalGameInfo(gameId);
                postBackModel.ludoPlayers = postedgame.ludoPlayers;
                postBackModel.numberOfPlayers = postedgame.numberOfPlayers;
                postBackModel.gameId = postedgame.gameId;
                Log.Information("Added player with name: {nameOfPlayer}" + " with color: {playerColor}" + " and gameId: {gameId}", model.ludoplayer.name, model.ludoplayer.playerColor, gameId);
                return View("GameConfiguration", postBackModel);
            }
            else
            {
                postBackModel.ludoPlayers = game.ludoPlayers;
                postBackModel.numberOfPlayers = game.numberOfPlayers;
                postBackModel.gameId = game.gameId;

                return View("GameConfiguration", postBackModel);
            }

            


        }
    }
}