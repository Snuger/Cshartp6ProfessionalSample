using CrazyElemphant.Client.Contracts.IServices;
using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyElemphant.Client.Contracts;
using System.Collections.ObjectModel;

namespace CrazyElemphant.Client.Services
{
   public class DishService:IDishService
    {
        public IDishRespostry DishRespostry { get; set; } 
        
        public DishService(IDishRespostry dishRespostry)
        {
            DishRespostry = dishRespostry;
        }

        public Dish GetDishByID(int id) => DishRespostry.GetItemByID(id);

        public void DeleteDishByID(int id) => DishRespostry.Delete(id);

        public List<Dish> GetItems() => DishRespostry.GetItems();



              
    }
}
