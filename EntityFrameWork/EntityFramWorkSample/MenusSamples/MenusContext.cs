using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MenusSamples
{
    public class MenusContext : DbContext
    {
        private const string ConnectString = "server=192.168.188.102;userid=root;pwd=admin;port=3306;database=MenusContext;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(ConnectString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

           // modelBuilder.HasDefaultSchema("mc");
            modelBuilder.Entity<MenuCard>().ToTable("MenuCards")
                .HasKey(c => c.MenuCardId);
            modelBuilder.Entity<MenuCard>().Property<string>(c => c.Title)
                .HasMaxLength(50);
            modelBuilder.Entity<MenuCard>().Property<int>(c => c.MenuCardId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Menu>().ToTable("Menus")
                .HasKey(c => c.MenuId);
            modelBuilder.Entity<Menu>().Property<int>(c => c.MenuId)
                .ValueGeneratedOnAdd();
            //modelBuilder.Entity<Menu>()
            //    .Property<decimal?>(m => m.Price)
             //   .HasColumnType("Money");

            modelBuilder.Entity<MenuCard>()
                .HasMany(c => c.Menus)
                .WithOne(m => m.MenuCard);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.MenuCard)
                .WithMany(c => c.Menus);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


       public  DbSet<Menu> Menus { get; set; }

       public DbSet<MenuCard> MenuCard{get;set;}
    }
}
