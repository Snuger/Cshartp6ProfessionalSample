using Dapper;
using System.Data;
using System.Threading.Tasks;
using Template.ORM;
using Template.Service.IServices;
using Template.Service.QueryParameter;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Template.ORM.IRepositories;
using Template.ORM.Models.Sample;

namespace Template.Service.Services
{
    public class SampleMSFEFCoreSevice : ISampleMSFEFCoreSevice
    {

        private readonly ISampleModelRepository _repository;

        public SampleMSFEFCoreSevice(ISampleModelRepository repository)
        {
            _repository = repository;
        }       

        public async Task<SampleModel> GetSampleMSFEFCoreMethodAsync(string id)=> await _repository.GetByIdAsync(id);

    }

}