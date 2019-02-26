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
        [Required(ErrorMessage = "Please enter a user name.")]
        [RegularExpression(@"^[a-zA-ZåäöüßÅÄÖÜ]+$", ErrorMessage = "Name can only contain letters")]
        [StringLength(5 - 15, ErrorMessage = "The First Name must be more than {1} characters and less tha {2}.")]
        public string Name { get; set; }
        public int gameId { get; set; }
    }
}
