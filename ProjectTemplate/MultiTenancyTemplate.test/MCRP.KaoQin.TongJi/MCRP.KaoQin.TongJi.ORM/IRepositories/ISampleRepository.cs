 
using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using MCRP.KaoQin.TongJi.ORM.Models;

namespace MCRP.KaoQin.TongJi.ORM.IRepositories
{
    public interface ISampleRepository : IEfCoreRepository<SampleModel, string>
    {
    }
}
