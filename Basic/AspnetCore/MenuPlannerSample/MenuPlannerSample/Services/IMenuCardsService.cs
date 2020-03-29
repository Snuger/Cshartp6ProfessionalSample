using MenuPlannerSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlannerSample.Services
{
    public interface IMenuCardsService
    {
        Task AddMenuAsync(Menu menu);

        Task DelteMenuAsync(int id);

        Task<Menu> GetMenuByIdAsync(int id);

        Task<IEnumerable<Menu>> GetMenusAsync();

        Task<IEnumerable<MenuCard>> GetMenuCardsAsync();

        Task UpdateMenuAsync(Menu menu);

        Task AddMenuCardAsync(MenuCard card);
    }
}
