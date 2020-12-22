using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mcrp.shujuzt.customer.API.IServices;
using Mcrp.shujuzt.customer.ORM.Models;
using Mcrp.shujuzt.customer.ORM.Repositories;

namespace Mcrp.shujuzt.customer.API.Services
{
    public class SampleService:ISampleService
    {
        private readonly IEfCoreRepository<SampleModel, string> _repository;
        public SampleService(IEfCoreRepository<SampleModel, string> repository )
        {
            _repository = repository;
        }

        public async Task<SampleModel> Get(string id)
        {
          return  await _repository.GetAsync(id);
        }
    }
}
