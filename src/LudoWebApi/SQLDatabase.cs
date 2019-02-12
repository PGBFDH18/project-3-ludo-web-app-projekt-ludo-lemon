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

        /// <summary>
        /// Loads all GameModels from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GameModel> Load()
        {
            var command = new SqlCommand("SELECT * FROM LudoGame", _connection);
            _connection.Open();
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                yield return new GameModel
                {
                    GameId = Convert.ToInt32(data["ID"]),
                    State = Convert.ToString(data["State"]),
                    CurrentPlayerId = Convert.ToInt32(data["CurrentPlayerID"])
                };
            }
            data.Close();
        }

        /// <summary>
        /// Loads a single GameModel from database.
        /// </summary>
        /// <param name="gameID">ID of the game to load from database.</param>
        /// <returns></returns>
        public GameModel Load(int gameID)
        {
            var parameters = new { gID = gameID };            

            GameModel gameModel = _connection.QueryFirst<GameModel>(
                "SELECT " +
                "   ID, " +
                "   State, " +
                "   CurrentPlayerID " +
                "FROM LudoGame " +
                "WHERE ID = @gID", parameters);

            LudoPlayer[] players = _connection.Query<LudoPlayer>(
                "SELECT * " +
                "FROM LudoGame AS lg " +
                "JOIN PlayerLudoGame AS plg ON lg.ID = plg.LudoGameID " +
                "JOIN Player AS pl ON plg.PlayerID = pl.ID " +
                "WHERE lg.ID = @gID AND ", parameters).ToArray();

            LudoGameEngine.Piece[] pieces = _connection.Query<LudoGameEngine.Piece>(
                "SELECT * " +
                "FROM LudoGame AS lg " +
                "JOIN PlayerLudoGame AS plg ON lg.ID = plg.LudoGameID " +
                "JOIN Player AS pl ON plg.PlayerID = pl.ID " +
                "JOIN Piece AS pi ON pl.ID = pi.PlayerID " +
                "WHERE lg.ID = @gID", parameters).ToArray();

            throw new NotImplementedException();
        }

        public void Remove(int gameID)
        {
            throw new NotImplementedException();
        }
        public void Update(int gameID, GameModel gameModel)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void DropConnection() => _connection.Close();
    }
}
