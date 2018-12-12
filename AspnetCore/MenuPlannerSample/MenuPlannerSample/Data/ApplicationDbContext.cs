using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MenuPlannerSample.Models;

namespace MenuPlannerSample.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        public DbSet<SossaManager> SossaManagers {get;set;}

        public DbSet<H3cData> H3CDatas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var menus = builder.Entity<Menu>();
            menus.Property(p => p.Text).HasMaxLength(50).IsRequired();
            menus.Property(p => p.Active).HasColumnType("bit");

            var menuCard = builder.Entity<MenuCard>();
            menuCard.Property(p => p.Active).HasColumnType("bit");

            var appUser = builder.Entity<ApplicationUser>();
            appUser.Property(p => p.EmailConfirmed).HasColumnType("bit");
            appUser.Property(p => p.PhoneNumberConfirmed).HasColumnType("bit");
            appUser.Property(p => p.TwoFactorEnabled).HasColumnType("bit");
            appUser.Property(p => p.LockoutEnabled).HasColumnType("bit");
            base.OnModelCreating(builder);
          
          

           
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
