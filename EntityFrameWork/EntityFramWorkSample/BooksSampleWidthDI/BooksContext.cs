using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksSampleWidthDI
{
    public class BooksContext : DbContext
    {
        public  BooksContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<Book> Books { get; set; }
    }
}
