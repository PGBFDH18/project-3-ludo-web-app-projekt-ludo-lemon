using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class CreateGameModel
    {
        public int gameId { get; set; }
        public int numberOfPlayers { get; set; }
        public LudoModifyPlayerModel ludoplayer { get; set; }
        public List<LudoPlayer> ludoPlayers { get; set; }
    }
    public class GameModel
    {
        public string state { get; set; }
        public int gameId { get; set; }
        public int numberOfPlayers { get; set; }
        public LudoPlayer ludoplayer { get; set; }
        public int currentPlayerId { get; set; }
        public List<LudoPlayer> ludoPlayers { get; set; }
        public Piece[] pieces { get; set; }
        public string winner { get; set; } = null;
    }
}
