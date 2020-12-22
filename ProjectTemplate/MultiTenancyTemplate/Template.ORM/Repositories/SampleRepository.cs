 
using MCRP.MultiTenancy.ORM.Repositories;
using Starter.AutoDI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Template.ORM.IRepositories;
using Template.ORM.Models;

namespace Template.ORM.Repositories
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
