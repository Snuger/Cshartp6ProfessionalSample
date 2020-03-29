using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationSample.Models;
using MvcApplicationSample.Extensions;

namespace MvcApplicationSample.Controllers
{
    public class HtmlHelperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HelperWithMenu() => View(GetSampleMenu());

        public IActionResult StronglyTypeMenu() => View(GetSampleMenu());
      
        private Menu GetSampleMenu() =>
            new Menu()
            {
                Id=1,
                Text="ASP.NET MVC 强类型模型绑定",
                Price=6.9,
                CreateDate=System.DateTime.Now,
                 Category="Main"
            };

        public IActionResult HelperList() {
            IEnumerable<Zoning> zonings = GetZonings();
            ViewBag.Zonings = zonings;
            return View(zonings.ToSelectListItem(0));
        }

        public IEnumerable<Zoning> GetZonings() => new List<Zoning>() {
            new Zoning(){Code=0,Name="中国",ParentCode=99},
            new Zoning(){ Code=1, Name="北京",ParentCode=0},
            new Zoning(){ Code=2, Name="上海",ParentCode=0},
            new Zoning(){ Code=3, Name="河北省",ParentCode=0},
            new Zoning(){ Code=301, Name="石家庄",ParentCode=3},
            new Zoning(){ Code=4, Name="浙江省",ParentCode=0},
            new Zoning(){ Code=401, Name="杭州",ParentCode=4},
             new Zoning(){ Code=5, Name="江苏省",ParentCode=0},
            new Zoning(){ Code=501, Name="南昌",ParentCode=5}
        };
    }
}