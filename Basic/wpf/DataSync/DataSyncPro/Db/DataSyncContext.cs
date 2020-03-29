using DataSyncPro.Db.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Db
{
    public class DataSyncContext : DbContext
    {
        public DataSyncContext():base("name=DataSyncPro")
        {
        }
        public DbSet<SynchronousDb> SynchronousDb { get; set; }

        public DbSet<SourceTable> SourceTables { get; set; }

    }
}
