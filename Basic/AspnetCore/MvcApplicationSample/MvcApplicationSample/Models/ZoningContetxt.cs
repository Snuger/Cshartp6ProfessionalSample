using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Models
{
    public class ZoningContetxt
    {
        private IEnumerable<Zoning> zonings = null;

        public IEnumerable<Zoning> Zonings => zonings ?? (zonings=new List<Zoning>() {   
            new Zoning(){Code=0,Name="中国",ParentCode=99},
            new Zoning(){ Code=1, Name="北京",ParentCode=0},
            new Zoning(){ Code=2, Name="上海",ParentCode=0},
            new Zoning(){ Code=3, Name="河北省",ParentCode=0},
            new Zoning(){ Code=301, Name="石家庄",ParentCode=3},
            new Zoning(){ Code=4, Name="浙江省",ParentCode=0},
            new Zoning(){ Code=401, Name="杭州",ParentCode=4},
             new Zoning(){ Code=5, Name="江苏省",ParentCode=0},
            new Zoning(){ Code=501, Name="南昌",ParentCode=5}
        });
    }
 }

