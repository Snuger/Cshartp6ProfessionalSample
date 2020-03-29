using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcApplicationSample.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContentDemo() => Content("hello word", "text/plan");


        public IActionResult JsonDemo(){
            return Json( 
                new {
                    id =3,
                    Text ="Griled sausage with  sauerkraut and potatoes",
                    price=12.90,
                    date=System.DateTime.Now,
                    category="Main"
            }); 
        }

        public IActionResult RedirectDemo() => Redirect("http://www.baidu.com");


        public IActionResult RedirectRouteDemo() => RedirectToRoute(new {controller="Home",action="Index" });

        public IActionResult FileDemo() => File("~/Images/Mathias.jpg","image/jpeg");
    }
}