using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public class ChickenBurger:Burger
    {
        public override float Price { get => 30.0f; }

        public override string Name { get => "Chicken Burger"; }
    }
}
