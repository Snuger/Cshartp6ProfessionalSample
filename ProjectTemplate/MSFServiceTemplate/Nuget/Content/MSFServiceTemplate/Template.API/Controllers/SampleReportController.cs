using System.Data;
using System.Threading.Tasks;
using Template.Service.IServices;
using Template.Service.QueryParameter;
using Microsoft.AspNetCore.Mvc;
using FastReport.Web;
using System.IO;
using System.Text;
using Template.API.Constants;

namespace Template.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SampleReportController : Controller
    {

        protected readonly ISampleMSFDapperSevice _dapperMSFService;

        public SampleReportController(ISampleMSFDapperSevice dapperMSFService)
        {
            _dapperMSFService = dapperMSFService;
        }



        /// <summary>
        /// 查询所有的工作量
        /// </summary>
        /// <param name="reportParameter"></param>
        /// <returns></returns>

        [HttpGet("all")]
        public async Task<IActionResult> GetGongZuoLTJAsync([FromQuery] SampleReportParameter reportParameter)
        {
            string reportFile = Path.Combine(SampleReportConstant.CONST_REPORT_FRX_ROOT, $"jixiaotj-global-{reportParameter.ReportName.ToLower().ToString()}.frx");
            if (string.IsNullOrWhiteSpace(reportParameter.ReportName))
                return BadRequest($"报表名{reportParameter.ReportName}-不存在");
            if (!System.IO.File.Exists(reportFile))
                return BadRequest($"{reportFile},报表文件不存在");
            DataTable dataSet = await _dapperMSFService.GetSampleMSFDapperMethodAsync(reportParameter);
            WebReport WebReport = new WebReport();
            WebReport.Report.Load(reportFile);
            WebReport.Report.RegisterData(dataSet, "Reports");
            ViewBag.WebReport = WebReport;
            return View("Report");
        }


    }
}