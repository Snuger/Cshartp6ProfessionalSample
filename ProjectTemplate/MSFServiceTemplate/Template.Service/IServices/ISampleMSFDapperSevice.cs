using System.Data;
using System.Threading.Tasks;
using Template.Service.QueryParameter;

namespace Template.Service.IServices
{
    public interface ISampleMSFDapperSevice
    {
        /// <summary>
        /// 获取开发人员绩效统计
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns>返回绩效统计的报表数据源</returns>
        Task<DataTable> GetSampleMSFDapperMethodAsync(SampleReportParameter reportParameter);



      

    }
}