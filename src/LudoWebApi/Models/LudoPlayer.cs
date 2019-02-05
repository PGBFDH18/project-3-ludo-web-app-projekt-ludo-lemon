using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameEngine;

namespace LudoWebApi.Models
{
    public class LudoPlayer
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public Piece[] Pieces { get; set; }
    }
}
