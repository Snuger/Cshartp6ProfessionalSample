using System;
using System.Data;

namespace BuilderDesigin
{
    public class DbContextOptions
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContextOptions(string connectionString)
        {
           ConnectionString = connectionString;            
        }

        public Guid OptionsGuid = System.Guid.NewGuid();

        public string ConnectionString { get; set; }


        public IDbConnection Connection { get; set; }

    }
}