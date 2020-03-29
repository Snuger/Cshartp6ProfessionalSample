using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcApplicationSample.Controllers
{
    public class HelplerMethodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleHelper() => View();
    }
}