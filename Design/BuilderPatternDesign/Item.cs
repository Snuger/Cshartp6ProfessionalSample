using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public interface Item
    {
        string Name { get; set; }

        public abstract Packing Packing();

        float Price { get; set; }

    }
}
