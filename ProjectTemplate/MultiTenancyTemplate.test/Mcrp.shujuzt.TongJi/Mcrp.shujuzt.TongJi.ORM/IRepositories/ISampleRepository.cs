 
using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Mcrp.shujuzt.TongJi.ORM.Models;

namespace Mcrp.shujuzt.TongJi.ORM.IRepositories
{
    public interface ISampleRepository : IEfCoreRepository<SampleModel, string>
    {
    }
}
