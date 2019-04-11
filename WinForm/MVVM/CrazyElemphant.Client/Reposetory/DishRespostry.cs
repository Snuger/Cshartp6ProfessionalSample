using CrazyElemphant.Client.Contracts;
using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrazyElemphant.Client.Reposetory
{
    public class DishRespostry : IDishRespostry
    {    
        public Task<Dish> Add(Dish item)
        {
            throw new NotImplementedException();
        }

        public Task<Dish> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Dish GetItemByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Dish> GetItems()
        {
            string xmlFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\Data.xml");
            XDocument xDoc = XDocument.Load(xmlFileName);
            List<Dish> Dishes = new List<Dish>();
            var dishes = xDoc.Descendants("Dish");
            foreach (var d in dishes)
            {
                Dish dish = new Dish();
                dish.Name = d.Element("Name").Value;
                dish.Categray = d.Element("Category").Value;
                dish.Commont = d.Element("Comment").Value;
                dish.UnitPrice =double.Parse(d.Element("UnitPrice").Value);
                dish.Scort = d.Element("Score").Value;
                Dishes.Add(dish);
            }
            return Dishes;
        }

        public Task<Dish> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Dish> GetItemsByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Dish> Update(Dish item)
        {
            throw new NotImplementedException();
        }
    }
}
