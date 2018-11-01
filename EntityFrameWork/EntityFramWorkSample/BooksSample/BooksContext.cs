using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksSample
{
    public  class BooksContext:DbContext
    {
        private const string connectionString = "server=192.168.188.102;userid=root;pwd=admin;port=3306;database=db_sync;";

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(connectionString);
        }
    }

}
