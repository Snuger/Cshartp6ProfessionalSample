using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationSample.Models;

namespace MvcApplicationSample.Controllers
{
    public class SubmitDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateMenus() => View();
        public IActionResult CreateMenus2() => View();
        public IActionResult CreateMenus3() => View();
        public IActionResult CreateMenus4() => View();


    
        public IActionResult CreateMenu(int id, string text, double price, string category) {
            Menu menu = new Menu { Id = id, Text = text, Price = price, Category = category };
            ViewBag.info = $"menu created: {menu.Text}, Price: {menu.Price}, category: {menu.Category}";
            return View("index");
        }

        public IActionResult CreateMenu2(Menu menu) {
            ViewBag.info = $"menu created: {menu.Text}, Price: {menu.Price}, category: {menu.Category}";
            return View("index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu3Result() {
            Menu menu = new Menu();
            bool updated = await TryUpdateModelAsync<Menu>(menu);
            if (updated)
            {
                ViewBag.info = $"menu created: {menu.Text}, Price: {menu.Price}, category: {menu.Category}";
                return View("index");
            }
            else {
                return View("Error");
            }
        }

        public IActionResult CreateMenu4(Menu menu)
        {
            if (ModelState.IsValid) {
                ViewBag.info = $"menu created: {menu.Text}, Price: {menu.Price}, category: {menu.Category}";
            }
            else
            {
                ViewBag.info = "not valid";
            }
            return View("index");
        }
    }
}