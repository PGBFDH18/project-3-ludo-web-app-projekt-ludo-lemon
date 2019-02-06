using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoWebApi
{
    interface IDatabase
    {
        void Save();
        IEnumerable<Models.GameModel> Load();
        Models.GameModel Load(int gameID);
        void Update(int gameID, Models.GameModel gameModel);
        void Remove(int gameID);
    }
}
