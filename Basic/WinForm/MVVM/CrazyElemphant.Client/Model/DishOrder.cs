using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.Model
{
    public class DishOrder
    {
        public string Name { get; set; }

        public string Categray { get; set; }

        public double UnitPrice { get; set; }

        public int Count { get; set; }
    }
}
