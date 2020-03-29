using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncPro.Contract.IRepository;
using DataSyncPro.Db.Entity;

namespace DataSyncPro.Contract.IService
{
    public interface ISourceTableService:IUpdateService<SourceTable,int>,IQueryService<SourceTable,int>
    { 

    }
}
