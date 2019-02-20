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
        private string _connectionString = $"Data Source=den1.mssql8.gear.host;Initial Catalog=lemon;Persist Security Info=True;User ID=lemon;Password={System.IO.File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "key.dbpass"))}";

        // GET: DatabaseTest/
        [HttpGet]
        public ActionResult<IEnumerable<Models.GameModel>> LoadAll()
        {
            SQLDatabase database = 
                new SQLDatabase(_connectionString);
            var hello = database.LoadGames();

            return Ok(hello);
        }

        // GET: DatabaseTest/123
        [Route("{gameId}")]
        [HttpGet]
        public ActionResult<Models.GameModel> Load(int gameId)
        {
            SQLDatabase database = 
                new SQLDatabase(_connectionString);
            var hello = database.LoadGame(gameId);

            return Ok(hello);
        }

        [HttpPut]
        public ActionResult<Models.GameModel> Update(Models.GameModel gameModel)
        {
            SQLDatabase database =
                new SQLDatabase(_connectionString);
            database.UpdateGame(gameModel);

            return Ok(database.LoadGame(gameModel.GameId));

        }

    }
}
