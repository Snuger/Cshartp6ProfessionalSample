using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataSyncPro.Model
{
    /// <summary>
    /// 功能菜单
    /// </summary>
    public class BasicMenu
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 名称 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// 父级编码
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public PackIconKind Kiind { get; set; }


        /// <summary>
        /// 用户菜单地址
        /// </summary>
        public object Content { get; set; }


    }
}
