 
using MCRP.MultiTenancy.ORM.Repositories;
using Starter.AutoDI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Mcrp.shujuzt.TongJi.ORM.IRepositories;
using Mcrp.shujuzt.TongJi.ORM.Models;

namespace Mcrp.shujuzt.TongJi.ORM.Repositories
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
