using System;
using System.Collections.Generic;
using System.Text;

namespace Template.API.RemoteServices.Rsos
{
    public class SampleRso
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string JueSeID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string JueSeMC { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public string GongNengID { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string GongNengMC { get; set; }

        ///功能点属性
        public string GongNengDSX { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        public long? ShunXuHao { get; set; }
    }
}
