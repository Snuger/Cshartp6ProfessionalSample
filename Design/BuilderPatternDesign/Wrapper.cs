using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public class Wrapper : Packing
    {
        public string Pack()
        {
            return "Wrapper";
        }
    }
}
