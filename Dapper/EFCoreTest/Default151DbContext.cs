using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EFCoreTest
{
    public class Default151DbContext : DbContext
    {




        public DbSet<KaiFaRw> KaiFaRwModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
          {     
            options.UseNpgsql("Uid=mcrp;Pwd=mcrp;Server=172.19.30.151;Port=5432;Database=mcrpdb;Search Path=mcrp_dev;");
            options.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
                modelBuilder.Entity(entity.Name, builder =>
                {
                   foreach (var property in entity.GetProperties()) {
                        builder.Property(property.Name).HasColumnName(property.Name.ToLower());
                    }
                });
            }
            modelBuilder.Entity<KaiFaRw>().ToTable("dev_kaifarw").HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }


    public class KaiFaRw
    {
        public string Id { get; set; }

        public string  ChanPinID { get; set; }
 
	    public string ZiXitID { get; set; }

	    public string ZiXitMC { get; set; }
    }
}