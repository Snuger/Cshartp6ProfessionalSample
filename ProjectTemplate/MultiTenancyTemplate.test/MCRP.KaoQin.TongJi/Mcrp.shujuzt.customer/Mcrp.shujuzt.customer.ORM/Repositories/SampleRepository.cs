 
using MCRP.MultiTenancy.ORM.Repositories;
using Starter.AutoDI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Mcrp.shujuzt.customer.ORM.IRepositories;
using Mcrp.shujuzt.customer.ORM.Models;

namespace Mcrp.shujuzt.customer.ORM.Repositories
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
