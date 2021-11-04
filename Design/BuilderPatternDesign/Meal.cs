using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public class Meal
    {
        private List<Item> _items;
        public Meal()
        {
            _items = new List<Item>(); 

        }        

        public void AddItem(Item item) => _items.Add(item);
        public float getCost() =>_items.Sum(u => u.Price);
       
        public void ShowItems()
        {


        }


    }
}
