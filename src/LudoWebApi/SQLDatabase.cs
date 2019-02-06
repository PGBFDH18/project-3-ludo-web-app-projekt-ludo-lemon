using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LudoWebApi.Models;

namespace LudoWebApi
{
    public class SQLDatabase : IDatabase
    {
        private SqlConnection _connection;

        public SQLDatabase(string connectionString) => 
            _connection = new SqlConnection(connectionString); 

        public IEnumerable<GameModel> Load()
        {
            throw new NotImplementedException();
        }

        public GameModel Load(int gameID)
        {
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
    }
}
