using System;
using System.Data;
using Dapper;
using MySql.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Threading;

namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceCollection services = new ServiceCollection();
            services.AddScoped(typeof(DbContext), ac => new DbContext("Server=172.21.9.248;port=3306;Database=northwind; User=root;Password=123456"));

            ServiceProvider serviceProvider = services.BuildServiceProvider();         

         
            for (int i = 0; i <= 28; i++) {
                var dbContext = (DbContext)serviceProvider.GetRequiredService(typeof(DbContext));
                var obj1 = dbContext.Connection.Query($"select  * from customers c limit 1 offset {i} ");
                Console.WriteLine($"{i}->{dbContext.guid.ToString()}-{dbContext.GetHashCode()}->{JsonSerializer.Serialize(obj1)}->{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                Thread.Sleep(100);
            }


            var dbContext1 = (DbContext)serviceProvider.GetRequiredService(typeof(DbContext));
            var obj2 = dbContext1.Connection.Query($"select  * from customers c limit 1 offset 1 ");
             Console.WriteLine($"未尾->{dbContext1.guid.ToString()}-{dbContext1.GetHashCode()}->{JsonSerializer.Serialize(obj2)}->{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
        }
    }
}
