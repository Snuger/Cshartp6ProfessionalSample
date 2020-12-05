using System.Data;
using System.Threading.Tasks;
using Template.ORM.Models.Sample;
using Template.Service.QueryParameter;

namespace Template.Service.IServices
{
    public interface ISampleMSFEFCoreSevice
    {
        Task<SampleModel> GetSampleMSFEFCoreMethodAsync(string id);

    }
}