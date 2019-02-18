using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class GameConfiguration
    {
        public int gameId { get; set; }
        
        public GameConfiguration(int gameID)
        {
            gameId = gameID;
        }
    }
}
