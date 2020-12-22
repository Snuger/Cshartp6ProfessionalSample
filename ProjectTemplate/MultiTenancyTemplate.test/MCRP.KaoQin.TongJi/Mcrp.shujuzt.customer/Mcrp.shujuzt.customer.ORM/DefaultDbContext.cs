using JetBrains.Annotations;
using MCRP.MSF.Core.UnitOfWork.Abstraction; 
using MCRP.MultiTenancy.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mcrp.shujuzt.customer.ORM.Models;

namespace Mcrp.shujuzt.customer.ORM
{
    public class DefaultDbContext : EfCoreDbContext<DefaultDbContext>, IUnitOfWork<DefaultDbContext>
    {
        public DbSet<SampleModel> JieKouRCs { get; set; }
        public DefaultDbContext([NotNull] DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SampleModel>(ac =>
            {
                ac.ToTable("samplemodel_table");
            });
        }
    }
}
