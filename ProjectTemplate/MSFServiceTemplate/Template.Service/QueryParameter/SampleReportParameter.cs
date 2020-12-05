namespace Template.Service.QueryParameter
{
    public class SampleReportParameter : SamplePatameterBase
    {
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
    }
}