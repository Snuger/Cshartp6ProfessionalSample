using MCRP.MSF.Core.UnitOfWork.Abstraction;
using MCRP.MSF.CRUD.ORM;
using MCRP.MSF.Service.Common.Services;
using MCRP.Utils.UID;
using Microsoft.EntityFrameworkCore;
using Template.ORM.EntityTypeConfigurations.Sample;
using Template.ORM.Models.Sample;

namespace Template.ORM
{
    public class DataDbContext : DbContextBaseImpl, IUnitOfWork<DataDbContext>
    {     
        public DbSet<SampleModel> SampleModels { get; set; }
 
        private readonly UIDWorker _worker;

        public DataDbContext(DbContextOptions<DataDbContext> options, UIDWorker worker, ITrackService trackService) : base(options, trackService)
        {
            _worker = worker;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        

            #region XiangMuZX
            modelBuilder.ApplyConfiguration(new SampleModelEntityTypeConfiguration(_worker));
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
