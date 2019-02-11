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
        public int RollDice()
        {
            //[Route("giveNumber")]
            var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            var request = new RestRequest("api/ludo/dice", Method.GET);
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource
            IRestResponse<int> ludoGameResponse = client.Execute<int>(request);
            var diceValue = ludoGameResponse.Data;
            //DiceModel model = new DiceModel(diceValue);
            Log.Information("The dice roll was: {diceValue}", diceValue);
            return diceValue;


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
        
        public IActionResult Index()
        {
            //[Route("giveNumber")]
            //var client = new RestClient("https://ludolemon-webapi.azurewebsites.net");

            //var request = new RestRequest("api/ludo/dice", Method.GET);
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource
            //IRestResponse<int> ludoGameResponse = client.Execute<int>(request);
            //var diceValue = ludoGameResponse.Data;
            //Console.WriteLine(diceValue);
            //Random e = new Random();
            //DiceModel model = new DiceModel(diceValue);

            return View();
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

        public void tryToFindAllPiecePositions()
        {
            //[Route("getGameInformation")]
            var client = new RestClient("http://localhost:50839/");

            var request = new RestRequest("/api/Ludo/115", Method.GET);
            //request.AddUrlSegment("id", gameID.ToString()); // replaces matching token in request.Resource
            IRestResponse ludoGameResponse = client.Execute(request);
            var gameSetup = ludoGameResponse;
            //PieceModel model = new PieceModel(gameSetup);
            string data = gameSetup.Content;
            JsonConvert.DeserializeObject<GameModel>(data);
            //return model;
        }
    }
}