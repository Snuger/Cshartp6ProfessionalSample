using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationSample.Models;

namespace MvcApplicationSample.Controllers
{
    public class TagHelpersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LabelHelper() => View(GetMenu());

        private Menu GetMenu() => new Menu
        {
            Id=1,
            Text="中国上下五千年",
            Price=99.5,
            CreateDate=System.DateTime.Now,
            Category="历史"
        };

        public IActionResult InputHelper() => View();



        public IActionResult FormHelper() => View();

        [HttpPost]
        public IActionResult FormHelper(Menu menu) {
            if (!ModelState.IsValid) {
                return View(menu);
            }
            return View("ValidationHelperResult",menu);
        }

        public IActionResult CustomerHelper() => View(GetMenus());

        private IEnumerable<Menu> GetMenus() => new List<Menu>()
        {
           new Menu {
            Id=1,
            Text="中国上下五千年",
            Price=99.5,
            CreateDate=System.DateTime.Now,
            Category="历史"},
            new Menu {
            Id=2,
            Text="易中天品三国",
            Price=99.5,
            CreateDate=System.DateTime.Now,
            Category="文学注解"},
            new Menu {
            Id=3,
            Text="诸葛亮传",
            Price=99.5,
            CreateDate=System.DateTime.Now,
            Category="人物"},
            new Menu {
            Id=4,
            Text="马云",
            Price=99.5,
            CreateDate=System.DateTime.Now,
            Category="人物"},
            new Menu {
            Id=5,
            Text="活在中国",
            Price=99.5,
            CreateDate=System.DateTime.Now,
            Category="生活"}
        };
    }
}