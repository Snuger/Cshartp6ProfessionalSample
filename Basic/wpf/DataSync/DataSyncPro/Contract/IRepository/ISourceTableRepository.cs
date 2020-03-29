using DataSyncPro.Db.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract.IRepository
{
    interface ISourceTableRepository:IQueryRepostry<SourceTable,int>,IUpdateRepostry<SourceTable,int>
    {
    }
}
