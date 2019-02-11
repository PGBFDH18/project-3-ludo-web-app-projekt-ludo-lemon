using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudoWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        // GET: api/DatabaseTest
        [HttpGet]
        public ActionResult<LudoWebApi.Models.GameModel> Get()
        {
            SQLDatabase database = 
                new SQLDatabase("Data Source=den1.mssql8.gear.host;Initial Catalog=lemon;Persist Security Info=True;User ID=lemon;Password=***********");
            var hello = database.Load(92384);

            return Ok(hello);
        }
    }
}
