using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSamples.Model
{
    public class FunctionalModule
    {

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }


        /// <summary>
        /// 模块下的功能组个
        /// </summary>
        public List<FunctionPoint> FunctionPoints { get; set; }

    }
}
