using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace LudoWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        // GET: DatabaseTest/
        [HttpGet]
        public ActionResult<IEnumerable<Models.GameModel>> LoadAll()
        {
            string password = System.IO.File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "key.dbpass"));
            SQLDatabase database = 
                new SQLDatabase($"Data Source=den1.mssql8.gear.host;Initial Catalog=lemon;Persist Security Info=True;User ID=lemon;Password={password}");
            var hello = database.LoadGames();

            return Ok(hello);
        }

        // GET: DatabaseTest/123
        [Route("{gameId}")]
        [HttpGet]
        public ActionResult<LudoWebApi.Models.GameModel> Load(int gameId)
        {
            string password = System.IO.File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "key.dbpass"));
            SQLDatabase database = 
                new SQLDatabase($"Data Source=den1.mssql8.gear.host;Initial Catalog=lemon;Persist Security Info=True;User ID=lemon;Password={password}");
            var hello = database.LoadGame(gameId);

            return Ok(hello);
        }

    }
}
