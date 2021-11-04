using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public  abstract class Burger : Item
    {
       public Packing Packing() {
            return new Wrapper();
       }

        public virtual  string Name { get;  set; }
        public virtual  float Price { get;  set; }        
        
    }
}
