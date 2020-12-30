using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EFCoreTest
{
    public class DefaultDbContext : DbContext
    {

        public DbSet<ShiWuGS> shiWuGSModels { get; set; }
        
        public DbSet<ZhiGongXX> ZhiGongXXModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseNpgsql("Uid=medi_oa;Pwd=medi_oa;Server=172.19.32.152;Port=5432;Database=medi_oa;Pooling=false; Timeout = 300;CommandTimeout = 300");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name, builder =>
                {
                    foreach (var property in entity.GetProperties())
                    {
                        builder.Property(property.Name).HasColumnName(property.Name.ToLower());
                    }
                });
            }
            modelBuilder.Entity<ZhiGongXX>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }

    }

    public class ShiWuGS
    {
        public string Id { get; set; }

    }


    public class ZhiGongXX {

        public string zhigongid { get; set; }
    }
}