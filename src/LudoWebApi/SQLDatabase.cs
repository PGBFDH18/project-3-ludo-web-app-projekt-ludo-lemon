using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LudoWebApi.Models;
using Dapper;

namespace LudoWebApi
{
    /// <summary>
    /// Provides essential tools for communicating with the lemon Transact-SQL database.
    /// </summary>
    public class SQLDatabase : IDatabase
    {
        private SqlConnection _connection;

        /// <summary>
        /// Initialises a Transact-SQL database object.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        public SQLDatabase(string connectionString) =>
            _connection = new SqlConnection(connectionString);

        /// <summary>
        /// Appends a user to the database.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user) =>
            _connection.Execute(
                "INSERT INTO [User] (ID, Username)" +
                "VALUES " +
                "   (@userID, @name)", new { userID = user.ID, name = user.Username });

        /// <summary>
        /// Loads an existing user from the database.
        /// </summary>
        /// <param name="userID">ID of the user to load.</param>
        /// <returns>A single instance of User.</returns>
        public User LoadUser(int userID) =>
            _connection.Query<User>(
                "SELECT ID, Username " +
                "FROM [User] " +
                "WHERE ID = @uID", new { uID = userID }).First();

        /// <summary>
        /// Loads all existing GameModel instances from the database.
        /// </summary>
        /// <returns>A collection of GameModel instances.</returns>
        public IEnumerable<GameModel> LoadGames()
        {
            var gameModels = _connection.Query<GameModel>(
                "SELECT " +
                "   ID AS GameId, " +
                "   State, " +
                "   CurrentPlayerId " +
                "FROM LudoGame").ToList();

            for (int i = 0; i < gameModels.Count; i++)
                gameModels[i].LudoPlayers = _connection.Query<LudoGameEngine.Player>(
                    "SELECT " +
                    "   pl.ID AS PlayerId, " +
                    "   u.Username AS Name, " +
                    "   pl.Color AS PlayerColor, " +
                    "   pl.Number AS Number " +
                    "FROM Player AS pl " +
                    "JOIN [User] AS u ON pl.UserID = u.ID " +
                    "JOIN PlayerLudoGame AS plg ON pl.ID = plg.PlayerID " +
                    "JOIN LudoGame AS lg ON plg.LudoGameID = lg.ID " +
                    "WHERE lg.ID = @lgID", new { lgID = gameModels[i].GameId }).ToArray();

            for (int i = 0; i < gameModels.Count; i++)
                for (int k = 0; k < gameModels[i].LudoPlayers.Length; k++)
                    gameModels[i].LudoPlayers[k].Pieces = _connection.Query<LudoGameEngine.Piece>(
                        "SELECT " +
                        "   pi.ID AS PieceId, " +
                        "   pi.State, " +
                        "   pi.Position " +
                        "FROM Piece AS pi " +
                        "JOIN Player AS pl ON pi.PlayerID = pl.ID " +
                        "WHERE pl.ID = @pID", new { pID = gameModels[i].LudoPlayers[k].PlayerId }).ToArray();
            
            return gameModels;
        }

        /// <summary>
        /// Loads a single existing GameModel instance from the database.
        /// </summary>
        /// <param name="gameID">ID of the game to load from the database.</param>
        /// <returns>A single GameModel instance.</returns>
        public GameModel LoadGame(int gameID)
        {
            var gameModel = _connection.QueryFirst<GameModel>(
                "SELECT " +
                "   ID AS GameId, " +
                "   State, " +
                "   CurrentPlayerId " +
                "FROM LudoGame " +
                "WHERE ID = @gID", new { gID = gameID });

            var players = _connection.Query<LudoGameEngine.Player>(
                "SELECT " +
                "   pl.ID AS PlayerId, " +
                "   u.Username AS Name, " +
                "   pl.Color AS PlayerColor, " +
                "   pl.Number AS Number " +
                "FROM Player AS pl " +
                "JOIN [User] AS u ON pl.UserID = u.ID " +
                "JOIN PlayerLudoGame AS plg ON pl.ID = plg.PlayerID " +
                "JOIN LudoGame AS lg ON plg.LudoGameID = lg.ID " +
                "WHERE lg.ID = @gID", new { gID = gameModel.GameId}).ToArray();

            for (int i = 0; i < players.Length; i++)
                players[i].Pieces = _connection.Query<LudoGameEngine.Piece>(
                    "SELECT " +
                    "   ID AS PieceId, " +
                    "   State," +
                    "   Position " +
                    "FROM Piece " +
                    "WHERE PlayerID = @pID", new { pID = players[i].PlayerId }).ToArray();

            gameModel.LudoPlayers = players;

            return gameModel;
        }

        /// <summary>
        /// Destroys an existing game in the database.
        /// </summary>
        /// <param name="gameID">ID of the game to destroy.</param>
        public void RemoveGame(int gameID)
        {
            // Cascade is used in the database.
            _connection.Execute(
                "DELETE Player " +
                "FROM Player " +
                "JOIN PlayerLudoGame ON Player.ID = PlayerLudoGame.PlayerID " +
                "JOIN LudoGame ON PlayerLudoGame.LudoGameID = LudoGame.ID " +
                "WHERE LudoGame.ID = @gID", new { gID = gameID });

            _connection.Execute(
                "DELETE LudoGame " +
                "WHERE ID = @lgID", new { lgID = gameID });
        }

        /// <summary>
        /// Updates an existing game in the database.
        /// </summary>
        /// <param name="gameModel">The modified GameModel instance.</param>
        public void UpdateGame(GameModel gameModel)
        {
            // Update one table at a time.
            _connection.Execute(
                "UPDATE LudoGame " +
                "SET " +
                "   State = @gState, " +
                "   CurrentPlayerID = @cpID " +
                "WHERE ID = @gID", new { gState = gameModel.State, cpID = gameModel.CurrentPlayerId, gID = gameModel.GameId });

            foreach (var player in gameModel.LudoPlayers)
                _connection.Execute(
                    "UPDATE Player " +
                    "SET " +
                    "   Color = @pColor, " +
                    "   Number = @pNumber " +
                    "WHERE ID = @pID", new { pColor = player.PlayerColor, pNumber = player.Number, pID = player.PlayerId });

            foreach (var player in gameModel.LudoPlayers)
                foreach (var piece in player.Pieces)
                    _connection.Execute(
                        "UPDATE Piece " +
                        "SET " +
                        "   State = @pState, " +
                        "   Position = @pPosition " +
                        "WHERE ID = @pID", new { pState = piece.State, pPosition = piece.Position, pID = piece.PieceId });
        }

        /// <summary>
        /// Adds a new game to the database.
        /// </summary>
        /// <param name="gameModel">The game model that will be appended to the database.</param>
        public void AddGame(GameModel gameModel)
        {
            // TODO: Discuss solutions.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        public void DropConnection() => _connection.Close();
    }
}
