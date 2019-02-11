using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LudoWebApi.Models;
using Dapper;

namespace LudoWebApi
{
    public class SQLDatabase : IDatabase
    {
        private SqlConnection _connection;

        public SQLDatabase(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void AddUser(IUser user)
        {
            _connection.Execute(
                "INSERT INTO [User] (ID, Username)" +
                "VALUES " +
                "   (@userID, @name)", new { userID = user.ID, name = user.Username });
        }

        public User LoadUser(int userID)
        {
            User user = _connection.Query<User>(
                "SELECT ID, Username " +
                "FROM [User] " +
                "WHERE ID = @uID", new { uID = userID}).First();

            return user;
        }

        /// <summary>
        /// Loads all GameModels from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GameModel> Load()
        {
            //var command = new SqlCommand("SELECT * FROM LudoGame", _connection);
            //_connection.Open();
            //SqlDataReader data = command.ExecuteReader();
            //while (data.Read())
            //{
            //    yield return new GameModel
            //    {
            //        GameId = Convert.ToInt32(data["ID"]),
            //        State = Convert.ToString(data["State"]),
            //        CurrentPlayerId = Convert.ToInt32(data["CurrentPlayerID"])
            //    };
            //}
            //data.Close();

            List<GameModel> gameModels = _connection.Query<GameModel>(
                "SELECT " +
                "   ID AS GameId, " +
                "   State, " +
                "   CurrentPlayerId " +
                "FROM LudoGame").ToList();

            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads a single GameModel from database.
        /// </summary>
        /// <param name="gameID">ID of the game to load from database.</param>
        /// <returns></returns>
        public GameModel Load(int gameID)
        {
            GameModel gameModel = _connection.QueryFirst<GameModel>(
                "SELECT " +
                "   ID AS GameId, " +
                "   State, " +
                "   CurrentPlayerId " +
                "FROM LudoGame " +
                "WHERE ID = @gID", new { gID = gameID });

            LudoGameEngine.Player[] players = _connection.Query<LudoGameEngine.Player>(
                "SELECT " +
                "   pl.ID AS PlayerId, " +
                "   pl.Color AS PlayerColor " +
                "FROM Player AS pl " +
                "JOIN PlayerLudoGame AS plg ON pl.ID = plg.PlayerID " +
                "JOIN LudoGame AS lg ON plg.LudoGameID = lg.ID " +
                "WHERE lg.ID = @gID", new { gID = gameModel.GameId}).ToArray();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].Pieces = _connection.Query<LudoGameEngine.Piece>(
                    "SELECT " +
                    "   ID AS PieceId, " +
                    "   State," +
                    "   Position " +
                    "FROM Piece " +
                    "WHERE PlayerID = @pID", new { pID = players[i].PlayerId }).ToArray();
            }

            gameModel.LudoPlayers = players;

            return gameModel;
        }

        public void Remove(int gameID)
        {
            throw new NotImplementedException();
        }
        public void Update(int gameID, GameModel gameModel)
        {
            throw new NotImplementedException();
        }

        public void Save(int userID, GameModel gameModel)
        {
            _connection.Execute("", new { });

            throw new NotImplementedException();
        }

        public void DropConnection() => _connection.Close();
    }
}
