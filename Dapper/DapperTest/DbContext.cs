using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

namespace DapperTest
{
    public  class DbContext:IDisposable
    {
        private string ConnectionString { get; set; }

        public IDbConnection Connection { get; }

        public Guid guid { get; set; } = new Guid();

        private bool isDisposed;

        public DbContext(string connectionString)
        {
            isDisposed = false;
            ConnectionString = connectionString;
            Connection = new MySqlConnection(connectionString);
        }

        public DbContext(IDbConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                ConnectionString = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
