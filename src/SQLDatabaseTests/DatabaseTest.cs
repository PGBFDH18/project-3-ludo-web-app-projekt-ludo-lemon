using System;
using Xunit;
using LudoWebApi;

namespace SQLDatabaseTests
{
    public class DatabaseTest
    {
        private bool _loadFromDatabaseReturnsCorrectPlayer_Passed = false;

        [Fact]
        public void LoadFromDatabaseReturnsCorrectPlayer()
        {
            SQLDatabase database = 
                new SQLDatabase("Data Source = den1.mssql8.gear.host; Initial Catalog = lemon; Persist Security Info = True; User ID = lemon; Password = ***********");

            LudoWebApi.Models.GameModel game = database.Load(92384);
            LudoGameEngine.Player player = game.LudoPlayers[0];

            _loadFromDatabaseReturnsCorrectPlayer_Passed = 3476 == player.PlayerId ? true : false;
            Assert.Equal(3476, player.PlayerId);
        }

        [Fact]
        public void SaveUserToDatabaseSuccessful()
        {
            IUser user = new User
            {
                ID = 123,
                Username = "TestUser"
            };

            // Write
            IDatabase database = 
                new SQLDatabase("Data Source=den1.mssql8.gear.host;Initial Catalog=lemon;Persist Security Info=True;User ID=lemon;Password=***********");
            database.AddUser(user);
        }

        [Fact]
        public void ReadUserFromDatabaseSuccessful()
        {
            IDatabase database =
                new SQLDatabase("Data Source=den1.mssql8.gear.host;Initial Catalog=lemon;Persist Security Info=True;User ID=lemon;Password=***********");

            // Read
            User extractedUser = database.LoadUser(123);

            Assert.Equal(123, extractedUser.ID);
            Assert.Equal("TestUser", extractedUser.Username);
        }
    }
}
