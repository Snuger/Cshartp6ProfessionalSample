 
using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Template.ORM.Models;

namespace Template.ORM.IRepositories
{
    public interface ISampleRepository : IEfCoreRepository<SampleModel, string>
    {
    }
}
