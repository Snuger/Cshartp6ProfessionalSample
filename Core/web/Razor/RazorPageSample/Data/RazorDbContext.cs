using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace RazorPageSample.Data
{
    public class RazorDbContext : DbContext
    {
        public RazorDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}