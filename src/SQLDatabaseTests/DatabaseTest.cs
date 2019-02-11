using System;
using Xunit;
using LudoWebApi;

namespace SQLDatabaseTests
{
    public class DatabaseTest
    {
        [Fact]
        public void LoadFromDatabaseReturnsCorrectPlayer()
        {
            SQLDatabase database = 
                new SQLDatabase("Data Source = den1.mssql8.gear.host; Initial Catalog = lemon; Persist Security Info = True; User ID = lemon; Password = ***********");

            LudoWebApi.Models.GameModel game = database.Load(92384);
            LudoGameEngine.Player player = game.LudoPlayers[0];

            Assert.Equal(3476, player.PlayerId);
        }
    }
}
