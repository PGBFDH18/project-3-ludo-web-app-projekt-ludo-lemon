using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class Piece
    {
        [Range(0,3)] 
        public int pieceId { get; set; }
        public int state { get; set; }
        public int position { get; set; }
    }
}

