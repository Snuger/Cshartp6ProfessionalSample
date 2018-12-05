using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;

namespace BooksSampleWidthDI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var p = new Program();
                p.initializeServices();

                var service = p.Container.GetService<BooksService>();
                service.AddBooksAsync().Wait();
                service.ReadBooks();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);               
            }
            Console.ReadLine();
        }

        private void initializeServices() {
            const string connectionString = @"server=192.168.188.102;userid=root;pwd=admin;port=3306;database=db_sync";
            var booksService = new ServiceCollection();

            booksService.AddTransient<BooksService>();
            booksService.AddDbContext<BooksContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            Container = booksService.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }
    }
}
