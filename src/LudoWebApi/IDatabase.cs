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
        void Save(int userID, Models.GameModel gameModel);
        IEnumerable<Models.GameModel> Load();
        Models.GameModel Load(int gameID);
        void Update(int gameID, Models.GameModel gameModel);
        void Remove(int gameID);
        void DropConnection();
    }
}
