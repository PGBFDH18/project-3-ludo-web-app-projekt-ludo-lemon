﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class LudoPlayer
    {
        public int playerId { get; set; }
        public string name { get; set; }
        public int playerColor { get; set; }
        public List<Piece> pieces { get; set; }
        public int offset { get; set; }
    }
}