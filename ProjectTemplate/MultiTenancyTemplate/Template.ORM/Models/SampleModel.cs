using MCRP.MSF.Core.Entities;
using MCRP.MSF.MultiTenancy;
using MCRP.MultiTenancy.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.ORM.Models
{
    public class SampleModel : MTEntity<string>
    {
        public string JieKouXXID { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public string MoRenZhi { get; set; }
        public string ZhuShi { get; set; }  
    }
}
