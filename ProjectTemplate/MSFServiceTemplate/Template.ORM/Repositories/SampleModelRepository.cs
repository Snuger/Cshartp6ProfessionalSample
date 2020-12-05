using MCRP.MSF.Core.CRUD.ORM;
using MCRP.MSF.CRUD.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Template.ORM.IRepositories;
using Template.ORM.Models.Sample;

namespace Template.ORM.Repositories
{
    public class SampleModelRepository : EfRepositoryImpl<SampleModel, string>, ISampleModelRepository
    {
        public SampleModelRepository(DbContextBase dbContext) : base(dbContext)
        {
        }
    }
}
