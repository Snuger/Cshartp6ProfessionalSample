using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.Contracts.IServices
{
    public interface IDishService
    {
         Dish GetDishByID(int id);

         void DeleteDishByID(int id);

         List<Dish> GetItems();

    }
}
