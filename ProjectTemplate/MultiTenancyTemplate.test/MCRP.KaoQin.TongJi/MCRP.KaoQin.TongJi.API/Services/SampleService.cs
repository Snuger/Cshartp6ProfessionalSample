using MCRP.MultiTenancy.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCRP.KaoQin.TongJi.API.IServices;
using MCRP.KaoQin.TongJi.ORM.Models;
using MCRP.KaoQin.TongJi.ORM.Repositories;

namespace MCRP.KaoQin.TongJi.API.Services
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
