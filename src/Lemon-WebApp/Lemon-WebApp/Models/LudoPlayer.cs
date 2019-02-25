using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon_WebApp.Models
{
    public class LudoPlayer
    {
        public int playerId { get; set; }
        [Required(ErrorMessage = "Please enter a user name.")]
        [RegularExpression(@"^[a-zA-ZåäöüßÅÄÖÜ]+$", ErrorMessage = "Name can only contain letters")]
        [StringLength(5-15, ErrorMessage = "The First Name must be more than {1} characters and less tha {2}.")]
        public string name { get; set; }
        public int playerColor { get; set; }
        public int Number { get; set; }
        public List<Piece> pieces { get; set; }
        public int offset
        {
            get => playerColor * 13;
        }
    }
}
