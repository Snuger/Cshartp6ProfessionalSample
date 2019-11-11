using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageSample.Data
{
    public class Order
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public double Cost { get; set; }


        public int CustomerId { get; set; }

        public int ProductId { get; set; }


    }
}
