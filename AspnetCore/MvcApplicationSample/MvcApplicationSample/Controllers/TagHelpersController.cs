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
    }
}