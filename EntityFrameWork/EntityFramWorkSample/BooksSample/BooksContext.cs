using JetBrains.Annotations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity("BooksSample.Book", b =>
            //{
            //    b.HasKey("BookId");


            //});
            var book = modelBuilder.Entity<Book>();
            book.HasKey(c => c.BookId);
            book.Property(p => p.Title).HasMaxLength(120).IsRequired();
            //mysql 版本下不可用
            // book.Property(p => p.TimeStamp).ValueGeneratedOnAddOrUpdate().IsRowVersion().IsConcurrencyToken();


            modelBuilder.Configurations.AddFromAssembly(typeof(BooksContext).Assembly);
            modelBuilder.Properties().Where(o => typeof(IRowVersion).IsAssignableFrom(o.DeclaringType) && o.PropertyType == typeof(byte[]) && o.Name == "RowVersion")
                .Configure(o => o.IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None));
            Database.SetInitializer(new MySqlDbInitializer());

        }
    }

}
