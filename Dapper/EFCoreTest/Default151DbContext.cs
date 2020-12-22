using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EFCoreTest
{
    public class Default151DbContext : DbContext
    {


        public DbSet<KaiFaRw> KaiFaRwModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseNpgsql("Uid=mcrp;Pwd=mcrp;Server=172.19.30.151;Port=5432;Database=mcrpdb;");
    }


    public class KaiFaRw
    {
        public string Id { get; set; }

    }
}