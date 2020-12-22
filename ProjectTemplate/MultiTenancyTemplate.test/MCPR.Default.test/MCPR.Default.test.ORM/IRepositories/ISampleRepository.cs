 
using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using MCPR.Default.test.ORM.Models;

namespace MCPR.Default.test.ORM.IRepositories
{
    public interface ISampleRepository : IEfCoreRepository<SampleModel, string>
    {
    }
}
