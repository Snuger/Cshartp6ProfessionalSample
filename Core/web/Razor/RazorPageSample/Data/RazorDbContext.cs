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

    }
}