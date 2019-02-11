using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoWebApi
{
    public interface IUser
    {
        int ID { get; set; }
        string Username { get; set; }
    }
}
