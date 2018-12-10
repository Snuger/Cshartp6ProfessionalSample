using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlannerSample.Data;
using MenuPlannerSample.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlannerSample.Services
{
    public class MenuCardsService : IMenuCardsService
    {
        private ApplicationDbContext applicationDbContext;

        public MenuCardsService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task AddMenuAsync(Menu menu)
        {
            applicationDbContext.Menus.Add(menu);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DelteMenuAsync(int id)
        {
            var menu = applicationDbContext.Menus.Single(m => m.Id==id);
            applicationDbContext.Menus.Remove(menu);
            await applicationDbContext.SaveChangesAsync();
        
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            return await applicationDbContext.Menus.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MenuCard>> GetMenuCardsAsync()
        {
            var menuCards = applicationDbContext.MenuCards;
            return await menuCards.ToArrayAsync();            
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            var menus = applicationDbContext.Menus;
            return await menus.ToArrayAsync();            
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            applicationDbContext.Entry(menu).State = EntityState.Modified;
            await applicationDbContext.SaveChangesAsync();
          
        }
    }
}
