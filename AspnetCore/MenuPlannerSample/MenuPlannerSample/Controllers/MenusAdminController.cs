using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MenuPlannerSample.Services;
using MenuPlannerSample.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MenuPlannerSample.Controllers
{
    public class MenusAdminController : Controller
    {
        private readonly IMenuCardsService menuCardsService;

        public MenusAdminController(IMenuCardsService menuCardsService)
        {
            this.menuCardsService = menuCardsService;
        }

        public async Task<IActionResult> Index()
        {
            return View((await menuCardsService.GetMenusAsync()).OrderBy(m=>m.Order));
        }

        public async Task<IActionResult> Details(int? id = 0) {
            if (id == null) {
                return BadRequest();
            }
            Menu menu = await menuCardsService.GetMenuByIdAsync(id.Value);
            if (menu == null) {
                return NotFound();
            }
            return View(menu);
        }

        public async Task<IActionResult> Create() {
            IEnumerable<MenuCard> cards = await menuCardsService.GetMenuCardsAsync();
            ViewBag.MenuCardId = new SelectList(cards,"id","Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind("Id", "MenuCardId", "Text", "Price", "Active", "Order", "Type", "Day")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await menuCardsService.AddMenuAsync(menu);
                return RedirectToAction("Index");
            }
            IEnumerable<MenuCard> cards = await menuCardsService.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards,"id","Name");
            return View(menu);
        }

    }
}