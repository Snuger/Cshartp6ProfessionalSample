using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public class VegBurger : Burger
    {
        public VegBurger()
        {
            Price = 25.0f;
            Name = "Veg Burger";
        }
    }
}
