using System;
using System.Collections.Generic;
using System.Text;

namespace LudoWebApi
{
    public class User : IUser
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public override string ToString() => Username;
    }
}
