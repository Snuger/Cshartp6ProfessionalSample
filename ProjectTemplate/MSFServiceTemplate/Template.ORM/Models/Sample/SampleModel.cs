using MCRP.MSF.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.ORM.Models.Sample
{
    public class SampleModel : DOEntityBase
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string LeiXing { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string KeHuID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string KeHuMC { get; set; }
        /// <summary>
        /// 客户/项目 ID
        /// </summary>
        public string XiangMuID { get; set; }
        /// <summary>
        /// 客户/项目 名称 
        /// </summary>
        public string XiangMuMC { get; set; }
        /// <summary>
        /// 项目产品/子系统产品id
        /// </summary>
        public string XiangMuCPID { get; set; }
        /// <summary>
        /// 项目产品/子系统产品名称
        /// </summary>
        public string XiangMuCPMC { get; set; }
        /// <summary>
        ///项目产品/子系统子系统ID
        /// </summary>
        public string XiangMuZXTID { get; set; }
        /// <summary>
        /// 项目产品/子系统子系统名称
        /// </summary>
        public string XiangMuZXTMC { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ChanPinID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ChanPinMC { get; set; }
        /// <summary>
        /// 子系统ID
        /// </summary>
        public string ZiXiTID { get; set; }
        /// <summary>
        /// 子系统名称
        /// </summary>
        public string ZiXiTMC { get; set; }
      

      
        /// <summary>
        /// 需求编号
        /// </summary>
        public string XuQiuBH { get; set; }
        /// <summary>
        /// 需求名称
        /// </summary>
        public string XuQiuMC { get; set; }
        /// <summary>
        /// 父需求ID-院方需求
        /// </summary>
        public string FuXuQID { get; set; }
        /// <summary>
        /// 父需求编号-院方需求
        /// </summary>
        public string FuXuQBH { get; set; }
        /// <summary>
        /// 父需求名称-院方需求
        /// </summary>
        public string FuXuQMC { get; set; }
        /// <summary>
        /// 重要性
        /// </summary>
        public string ZhongYaoXing { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string MiaoShu { get; set; }
      

        /// <summary>
        /// 需求正在处理的部门
        /// </summary>
        /// <value></value>
        public string BuMenID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string BuMenMC { get; set; }
       
        /// <summary>
        /// 状态（待审核、已拒绝、待实现、待确认、待测试、待验证、已关闭）
        /// </summary>
        public string ZhuangTai { get; set; }
       
        /// <summary>
        /// 任务状态（未完成、已完成）
        /// </summary>
        public string RenWuZT { get; set; }
        /// <summary>
        /// 期望完成日期
        /// </summary>
        public DateTime? QiWangWCRQ { get; set; }
       

        /// <summary>
        /// 外部缺陷标志
        /// </summary>
        public bool? WaiBuQXBZ { get; set; }
    }
}
