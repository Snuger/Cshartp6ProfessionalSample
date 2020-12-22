 
using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using MCRP.KaoQin.WeiHu.ORM.Models;

namespace MCRP.KaoQin.WeiHu.ORM.IRepositories
{
    public interface ISampleRepository : IEfCoreRepository<SampleModel, string>
    {
    }
}
