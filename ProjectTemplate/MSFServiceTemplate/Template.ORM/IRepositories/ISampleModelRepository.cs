using MCRP.MSF.Core.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Template.ORM.Models.Sample;

namespace Template.ORM.IRepositories
{
   public  interface ISampleModelRepository : IAsyncRepository<SampleModel, string>
    {
    }
}
