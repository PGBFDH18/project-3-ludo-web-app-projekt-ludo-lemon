using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class GameConfiguration
    {
        public List<int> games { get; set; }
        


        public GameConfiguration(List<int> game)
        {
            games = game;
        }
    }

    
}
