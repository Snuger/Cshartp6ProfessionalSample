using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sampke.Generator;

namespace Sampke.Orm.Models
{
    public partial class Student 
    {        
        public string? Age { get; set; }

        [AutoNotify]
        private string _name = string.Empty;
    }
}
