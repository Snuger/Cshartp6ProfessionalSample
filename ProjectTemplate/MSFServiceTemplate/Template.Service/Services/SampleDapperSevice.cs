using Dapper;
using System.Data;
using System.Threading.Tasks;
using Template.ORM;
using Template.Service.IServices;
using Template.Service.QueryParameter;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template.Service.Services
{
    public class SampleDapperSevice : ISampleMSFDapperSevice
    {

       private readonly  IDbConnection _dbConnection;
       private readonly DataDbContext _dataDbContext;

        public SampleDapperSevice(IDbConnection dbConnection, DataDbContext dataDbContext)
        {
            _dbConnection = dbConnection;
            _dataDbContext = dataDbContext;
        }

        public async Task<DataTable> GetSampleMSFDapperMethodAsync(SampleReportParameter reportParameter)
        {
            DataTable table = new DataTable(reportParameter.ReportName);
            StringBuilder sql = new StringBuilder(@"select '产品研发' as 开发类型, a.xuqiusjr AS 姓名,a.renwuly as 类型,sum(a.yujigs) as 标准工时,sum(a.shijigs) as  实际工时  from mcrp_prod.prod_renwu a  inner join mcrp_sys.sys_yonghuxx k on a.xuqiusjrid=k.id where k.zuofeibz=0 and k.yiyuanyhbz=0 and  a.zuofeibz=0 and  a.zhuangtai='已完成'");
            sql.Append(" and a.chanpinid=:ChanPinID");
            if (!string.IsNullOrEmpty(reportParameter.BuMenID))
                sql.Append(" and k.gongsiid=:BuMenID");
            sql.Append(" group by a.xuqiusjr,a.renwuly");
            var reader = await _dbConnection.ExecuteReaderAsync(sql.ToString(), reportParameter);
            table.Load(reader);
            return table;
        }    

    }

}