using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageSample.Data
{
    /// <summary>
    /// 类别
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public int ParentId { get; set; }

    }
}
