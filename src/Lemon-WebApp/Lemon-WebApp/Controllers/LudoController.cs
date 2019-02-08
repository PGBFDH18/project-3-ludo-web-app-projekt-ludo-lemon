using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemon_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Lemon_WebApp.Controllers
{
    public class LudoController : Controller
    {
        public int Something()
        {
            //[Route("createGame")]
            var client = new RestClient("http://localhost:50839/");

            var request = new RestRequest("api/ludo/", Method.POST);
            IRestResponse<int> ludoGameResponse = client.Execute<int>(request);
            var gameId = ludoGameResponse.Data;
            return gameId;
        }
        
        public IActionResult Index()
        {
            //[Route("giveNumber")]
            var client = new RestClient("http://localhost:50839/");

            var request = new RestRequest("api/ludo/dice", Method.GET);
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource
            IRestResponse<int> ludoGameResponse = client.Execute<int>(request);
            var diceValue = ludoGameResponse.Data;
            //Console.WriteLine(diceValue);
            //Random e = new Random();
            DiceModel model = new DiceModel(diceValue);

            return View(model);
        }

//<<<<<<< HEAD
//        public IActionResult GetPieces()
//        {
//            //[Route("collectPositions)]
//            var client = new RestClient("http://localhost:50839");
//            for (i = 0; i < )
//            var request = new RestRequest("api/ludo/29739/players/0", Method.GET)
//        }
//=======
        public int DeleteGame()  // (vi ska testa den när vi skapar spel)
        {
            //[Route("deleteGame")]
            var client = new RestClient("http://localhost:50839/");

            var request = new RestRequest("api/ludo/{gameId}", Method.DELETE);

            return HttpStatusCode(Ok);
        }

        private int HttpStatusCode(Func<OkResult> ok)
        {
            throw new NotImplementedException();
        }


//>>>>>>> 9fb847cfa099c3cf162afdc51ad089efbf56aa0a
    }
}