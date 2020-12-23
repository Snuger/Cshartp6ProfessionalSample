using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Linq;

namespace EFCoreTest
{



    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("查询151数据库");
            Default151DbContext context151 = new Default151DbContext();
             await  context151.KaiFaRwModels.FirstOrDefaultAsync();
            var objt151 = await context151.KaiFaRwModels.FromSqlRaw("select  * from  mcrp_dev.dev_kaifarw limit 1 offset 0").FirstOrDefaultAsync();
            Console.WriteLine(objt151.Id);

            Console.WriteLine("查询152 GP数据库");
            DefaultDbContext context = new DefaultDbContext();
             var objt=context.shiWuGSModels.FromSqlRaw("select * from cmp_wim_01.wim_shiwugs_0001 limit 100 offset 0");
            Console.WriteLine(objt?.FirstOrDefaultAsync().Id);

            Console.WriteLine("查询 应记工时 GP数据库");
            DefaultDbContext context33 = new DefaultDbContext();
            var objt33 = context33.shiWuGSModels.FromSqlRaw("select * from cmp_wim_01.wim_yingjigs_0001 limit 100 offset 0");
            Console.WriteLine(objt33?.FirstOrDefaultAsync().Id);



            Console.WriteLine("查询 应记工时 GP数据库");
            DefaultDbContext context44 = new DefaultDbContext();
            var objt44 = await context44.shiWuGSModels.FromSqlRaw("select * from cmp_wim_01.wim_yingjigs_0001 limit 1 offset 0").ToListAsync();
            Console.WriteLine(objt44.Count);



            Console.WriteLine("查询 应记工时 GP数据库");
            DefaultDbContext context88 = new DefaultDbContext();
            var objt88 = await context88.shiWuGSModels.FromSqlRaw("select  coalesce(a.zhigongid,b.tianbaoryid) as id,coalesce(a.zhigongxm,b.tianbaoryxm) as zhigongxm,coalesce(a.yingjigs,0) as jizhungs,coalesce(b.shiwugs,0) as shiwugs from(select  mwy.zhigongid,mwy.zhigongxm,sum(mwy.yingjigssl) as yingjigs  from  cmp_wim_01.wim_yingjigs_0001 mwy  where zuofeibz=0  group by mwy.zhigongid,mwy.zhigongxm) a full join (select ws.tianbaoryid,ws.tianbaoryxm,sum(ws.gongshisl) as shiwugs  from  cmp_wim_01.wim_shiwugs_0001  ws where zuofeibz =0  group by ws.tianbaoryid ,ws.tianbaoryxm) b  on a.zhigongid=b.tianbaoryid").ToListAsync();
            Console.WriteLine(objt88.Count);

         
            DefaultDbContext context1 = new DefaultDbContext();
            QueryDetailsParameter reportParameter = new QueryDetailsParameter();
            StringBuilder sql = new StringBuilder(@"select  coalesce(a.zhigongid,b.tianbaoryid) as zhigongid,coalesce(a.zhigongxm,b.tianbaoryxm) as zhigongxm,coalesce(a.yingjigs,0) as jizhungs,coalesce(b.shiwugs,0) as shiwugs from");
            sql.Append(@"(select  mwy.zhigongid,mwy.zhigongxm,sum(mwy.yingjigssl) as yingjigs  from  cmp_wim_01.wim_yingjigs_0001 mwy  where zuofeibz=0 ");
            if (!string.IsNullOrWhiteSpace(reportParameter.YongHuID))
                sql.Append(" and (mwy.zhigongxm=:XingMing or mwy.zhigongid=:XingMing)");
            if (reportParameter.TimeBegin.HasValue && reportParameter.TimeEnd.HasValue)
                sql.Append(" and mwy.nianyue>= :StrTimeBegin  and  mwy.nianyue<:StrTimeEnd ");
            if (!string.IsNullOrEmpty(reportParameter.BuMenID))
                sql.Append(" and mwy.gongsiid=:BuMenID");
            sql.Append(" group by mwy.zhigongid,mwy.zhigongxm) a");
            sql.Append(" full join ");
            sql.Append(@"(select ws.tianbaoryid,ws.tianbaoryxm,sum(ws.gongshisl) as shiwugs  from  cmp_wim_01.wim_shiwugs_0001  ws where zuofeibz =0 ");
            if (!string.IsNullOrWhiteSpace(reportParameter.YongHuID))
                sql.Append(" and (ws.tianbaoryxm=:XingMing or ws.tianbaoryid=:XingMing)");
            if (reportParameter.TimeBegin.HasValue && reportParameter.TimeEnd.HasValue)
                sql.Append(" and ws.gongshirq between  :TimeBegin and :TimeEnd");
            if (!string.IsNullOrEmpty(reportParameter.BuMenID))
                sql.Append(" and ws.gongsiid=:BuMenID"); 
            sql.Append(" group by ws.tianbaoryid ,ws.tianbaoryxm) b ");
            sql.Append(" on a.zhigongid=b.tianbaoryid");


           var otts=await context1.ZhiGongXXModels.FromSqlRaw(sql.ToString()).ToListAsync();

            Console.WriteLine(otts.Count);


            DefaultDbContext context2 = new DefaultDbContext();
            DataTable table = context2.Database.GetDataTable(sql.ToString(), null);
            Console.WriteLine(table.Rows.Count);

            Console.WriteLine("oh year");

        }


      
    }


    public class QueryDetailsParameter 
    {
        /// <summary>
        /// 用户ID 
        /// </summary>
        /// <value></value>
        public string YongHuID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>       
        public DateTime? TimeBegin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>  
        public DateTime? TimeEnd { get; set; }


        /// <summary>
        /// 获取查询时间段
        /// </summary>
        /// <returns></returns>
        public string GetQueryTimeRange() => (TimeBegin.HasValue && TimeEnd.HasValue) ? $"{TimeBegin.Value.ToString("yyyy-MM-dd")}~{TimeEnd.Value.ToString("yyyy-MM-dd")}" : "至今为止";

        /// <summary>
        /// 状态
        /// </summary>
        public string ZhuangTai { get; set; } = "已完成";


        /// <summary>
        /// 部门ID （对应公司信息表）
        /// </summary>
        /// <value></value>
        public string BuMenID { get; set; }

        /// <summary>
        /// 项目组ID(对应部门信息表)
        /// </summary>
        /// <value></value>
        public string XiangMuZID { get; set; }



        /// <summary>
        /// 开始时间
        /// </summary>  
        public string StrTimeBegin => TimeBegin?.ToString("yyyy-MM");

        /// <summary>
        /// 结束时间
        /// </summary> 
        public string StrTimeEnd => TimeEnd?.ToString("yyyy-MM");
    }
}

