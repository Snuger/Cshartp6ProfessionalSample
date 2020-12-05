using System;

namespace Template.Service.QueryParameter
{
    public class SamplePatameterBase
    {
        /// <summary>
        /// 报表名称(代码)
        /// </summary>
        public string ReportName { get; set; }


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
    }
}