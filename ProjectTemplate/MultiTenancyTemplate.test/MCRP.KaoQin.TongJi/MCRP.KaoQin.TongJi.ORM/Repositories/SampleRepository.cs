 
using MCRP.MultiTenancy.ORM.Repositories;
using Starter.AutoDI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using MCRP.KaoQin.TongJi.ORM.IRepositories;
using MCRP.KaoQin.TongJi.ORM.Models;

namespace MCRP.KaoQin.TongJi.ORM.Repositories
{
    [Repository]
    public class SampleRepository : EfCoreRepository<DefaultDbContext, SampleModel, string>,ISampleRepository
    {
        public SampleRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Test()
        {
            
        }
    }
}
