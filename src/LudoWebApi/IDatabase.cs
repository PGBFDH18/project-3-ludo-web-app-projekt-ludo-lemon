using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoWebApi
{
    public interface IDatabase
    {
        void AddUser(User user);
        User LoadUser(int userID);
        void AddGame(Models.GameModel gameModel);
        IEnumerable<Models.GameModel> LoadGames();
        Models.GameModel LoadGame(int gameID);
        void UpdateGame(Models.GameModel gameModel);
        void RemoveGame(int gameID);
        void DropConnection();
    }
}
