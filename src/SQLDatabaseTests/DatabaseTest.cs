using System;
using Xunit;
using LudoWebApi;
using System.IO;

// TEMP
namespace SQLDatabaseTests
{
    public class DatabaseTest
    {
        private bool _loadFromDatabaseReturnsCorrectPlayer_Passed = false;
        private string _connectionString = 
            $"Data Source = den1.mssql8.gear.host; Initial Catalog = lemon; Persist Security Info = True; User ID = lemon; Password = { File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "key.dbpass"))}";
        [Fact]
        public void LoadFromDatabaseReturnsCorrectPlayer()
        {
            SQLDatabase database = new SQLDatabase(_connectionString);

            LudoWebApi.Models.GameModel game = database.LoadGame(92384);
            LudoGameEngine.Player player = game.LudoPlayers[0];

            _loadFromDatabaseReturnsCorrectPlayer_Passed = 3476 == player.PlayerId ? true : false;
            Assert.Equal(3476, player.PlayerId);
        }

        [Fact]
        public void SaveUserToDatabaseSuccessful()
        {
            User user = new User
            {
                ID = 123,
                Username = "TestUser"
            };

            // Write
            IDatabase database = new SQLDatabase(_connectionString);
            database.AddUser(user);
        }

        [Fact]
        public void ReadUserFromDatabaseSuccessful()
        {
            IDatabase database = new SQLDatabase(_connectionString);

            // Read
            User extractedUser = database.LoadUser(123);

            Assert.Equal(123, extractedUser.ID);
            Assert.Equal("TestUser", extractedUser.Username);
        }

        [Fact]
        public void UpdateLudoGameSuccessful()
        {
            IDatabase database = new SQLDatabase(_connectionString);
            var existingGameModel = database.LoadGame(92384);
            existingGameModel.LudoPlayers[0].Pieces[0].Position += 10;
            int position = existingGameModel.LudoPlayers[0].Pieces[0].Position;

            database.UpdateGame(existingGameModel);

            existingGameModel = database.LoadGame(92384);

            Assert.Equal(position, existingGameModel.LudoPlayers[0].Pieces[0].Position);
        }

        [Fact]
        public void DeleteGameFromDatabaseSuccessful()
        {
            IDatabase database = new SQLDatabase(_connectionString);
            database.RemoveGame(3647);
        }
    }
}
