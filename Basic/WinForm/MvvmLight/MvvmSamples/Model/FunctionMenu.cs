using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSamples.Model
{
    public  class FunctionMenu
    {
        /// <summary>
        /// 功能菜单的根节点编码
        /// </summary>
        public string MenuRootCode { get; set; }

        /// <summary>
        /// 功能菜单的根节点名称
        /// </summary>
        public string MenuRootName { get; set; }

        /// <summary>
        /// 子模块
        /// </summary>
        public List<FunctionalModule> Modules { get; set; }

    }
}
