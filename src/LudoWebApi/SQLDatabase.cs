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

        public SQLDatabase(string connectionString) =>
            _connection = new SqlConnection(connectionString);

        public void AddUser(IUser user) =>
            _connection.Execute(
                "INSERT INTO [User] (ID, Username)" +
                "VALUES " +
                "   (@userID, @name)", new { userID = user.ID, name = user.Username });

        public User LoadUser(int userID) =>
            _connection.Query<User>(
                "SELECT ID, Username " +
                "FROM [User] " +
                "WHERE ID = @uID", new { uID = userID }).First();

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

            for (int i = 0; i < gameModels.Count; i++)
            {
                gameModels[i].LudoPlayers = _connection.Query<LudoGameEngine.Player>(
                    "SELECT " +
                    "   ID AS PlayerId, " +
                    "   Color AS PlayerColor " +
                    "FROM Player " +
                    "WHERE ID = @gID", new { gID = gameModels[i].GameId }).ToArray();
            }

            return gameModels;
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
                "   u.Username AS Name, " +
                "   pl.Color AS PlayerColor " +
                "FROM Player AS pl " +
                "JOIN [User] AS u ON pl.UserID = u.ID " +
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
            _connection.Execute(
                "DELETE FROM Piece " +
                "JOIN Player AS pl ON Piece.PlayerID = pl.ID " +
                "JOIN PlayerLudoGame AS plg ON pl.ID = plg.PlayerID " +
                "JOIN LudoGame AS lg ON plg.LudoGameID = lg.ID " +
                "WHERE lg.ID = @gID", new { gID = gameID });

            _connection.Execute(
                "DELETE PlayerLudoGame " +
                "WHERE LudoGameID = @lgID", new { lgID = gameID });

            var pieceIdsToDelete = _connection.Query<int>(
                "SELECT * " +
                "FROM LudoGame AS lg " +
                "JOIN PlayerLudoGame AS plg ON lg.ID = plg.LudoGameID " +
                "JOIN Player AS pl ON plg.PlayerID = pl.ID " +
                "JOIN Piece AS pi ON pl.ID = pi.PlayerID " +
                "WHERE lg.ID = @lgID", new { lgID = gameID });
        }
        public void Update(GameModel gameModel)
        {
            // Update one table at a time.
            _connection.Execute(
                "UPDATE LudoGame " +
                "SET " +
                "   State = @gState, " +
                "   CurrentPlayerID = @cpID " +
                "WHERE ID = @gID", new { gState = gameModel.State, cpID = gameModel.CurrentPlayerId, gID = gameModel.GameId });

            foreach (var player in gameModel.LudoPlayers)
            {
                _connection.Execute(
                    "UPDATE Player " +
                    "SET " +
                    "   Color = @pColor " +
                    "WHERE ID = @pID", new { pColor = player.PlayerColor, pID = player.PlayerId });
            }

            foreach (var player in gameModel.LudoPlayers)
                foreach (var piece in player.Pieces)
                {
                    _connection.Execute(
                        "UPDATE Piece " +
                        "SET " +
                        "   State = @pState, " +
                        "   Position = @pPosition " +
                        "WHERE ID = @pID", new { pState = piece.State, pPosition = piece.Position, pID = piece.PieceId });
                }
        }

        public void Save(GameModel gameModel)
        {
            var players = gameModel.LudoPlayers;
            var pieces = players.Select(pl => pl.Pieces).ToList();

            

            throw new NotImplementedException();
        }

        public void DropConnection() => _connection.Close();
    }
}
