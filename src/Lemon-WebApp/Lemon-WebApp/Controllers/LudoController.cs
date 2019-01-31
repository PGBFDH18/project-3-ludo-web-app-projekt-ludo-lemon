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
            DiceModel model = new DiceModel();
            model.currentDicePosition = model.DiceDictionary[diceValue];

            return View(model);
        }
    }
}