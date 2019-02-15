using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class MovePiece
    {
        public int playerId { get; set; }
        public int pieceId { get; set; }
        public int gameId { get; set; }
        public int numberOfFields { get; set; }
    }
}
