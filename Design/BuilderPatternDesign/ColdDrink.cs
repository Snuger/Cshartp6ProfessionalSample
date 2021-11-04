using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public class ColdDrink : Item
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public Packing Packing()
        {
            return new Bottle();
        }
    }
}
