using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lemon_WebApp.Models
{
    public class AddPlayer
    {
        public string Color { get; set; }

        public string Name { get; set; }
        public int gameId { get; set; }
    }
}
