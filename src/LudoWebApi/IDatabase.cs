using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoWebApi
{
    public interface IDatabase
    {
        void AddUser(IUser user);
        User LoadUser(int userID);
        void Save(Models.GameModel gameModel);
        IEnumerable<Models.GameModel> Load();
        Models.GameModel Load(int gameID);
        void Update(Models.GameModel gameModel);
        void Remove(int gameID);
        void DropConnection();
    }
}
