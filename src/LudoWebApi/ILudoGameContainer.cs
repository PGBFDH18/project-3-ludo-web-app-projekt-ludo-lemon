using LudoGameEngine;
using System.Collections.Generic;

namespace LudoWebApi
{
    public interface ILudoGameContainer
    {
        ILudoGame this[int gameId]
        {
            get;
        }
        //Dictionary<int, ILudoGame> GetGameIdsWithState();
        List<int> GetIdsOfAllGames();
        void DeleteGame(int id);
        void CreateGame(int gameId);
    }
}