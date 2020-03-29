using CrazyElemphant.Client.Contracts;
using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

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


        public bool DishPushOrder(List<DishOrder> dishOrders)
        {
            string directoryPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Odrder");
            StringBuilder stringBuilder = new StringBuilder();
            foreach(DishOrder order in dishOrders)
            {
                stringBuilder.Append($"{order.Name},{order.Count}\r");
            }
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            string xmlFileName = Path.Combine(directoryPath, @"order.log");
           if(! File.Exists(xmlFileName))            
                File.Create(xmlFileName);     
            File.WriteAllLines(xmlFileName, new string [] { stringBuilder.ToString() });
            return true;
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
