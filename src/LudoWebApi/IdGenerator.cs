using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoWebApi
{
    public class IdGenerator : IIdGenerator
    {
        public int GenerateId()
        {
            Random random = new Random();
            return random.Next(1, 50000);
        }
    }
}
