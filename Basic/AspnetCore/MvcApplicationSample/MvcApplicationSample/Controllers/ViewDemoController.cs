using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationSample.Models;

namespace MvcApplicationSample.Controllers
{
    public class ViewDemoController : Controller
    {
        private EventsAndMenusContext _context;

        public ViewDemoController(EventsAndMenusContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.MyData = "Hello from the controller";
            ViewData["MyData"] = "Hello from the controller";
            return View();
        }

        public IActionResult PassingMode() {
            Students students = new Students()
            {
                 Id=1,
                  Address="河南省内乡县",
                   Age=30,
                    Name="张三",
                     TelPhone="1362872890222"
            };
            return View(students);
        }

        public IActionResult PassingSet() {

            List<Students> students = new List<Students>()
            {
                new Students() {Id=1,Address="河南省内乡县",Age=30,Name="张三",TelPhone="1362872890222"},
                new Students() {Id=2,Address="河南省内乡县",Age=22,Name="李四",TelPhone="1362872890221"},
            };
            return View(students);
        }

        public IActionResult PassingData() {
            ViewBag.MyData = "hello from  conntroler";
            return View();
        }
        public IActionResult LayoutUsingSections() {

            return View();
        }

        public IActionResult LayoutSample() => View();

        public IActionResult UseAPartialview() => View(_context);

        public IActionResult UseAPartialview2() {
            return View();
        }

        public IActionResult ShowEvents() {
            ViewBag.EventsTitel = "局部视图";
            return PartialView(_context.Events);
        }

        public IActionResult UseViewComponent()=>View();

        public IActionResult UseZoningPickerSample() => View();
    }
}