using System;
using System.Collections.Generic;
using System.Text;

namespace EventSample
{
    public  class Trader
    {
        public Trader(string name)
        {
            this.name = name;
        }

        private string name { get; }

        public void NewCarIsHere(object sender, CarInfoEventArgs e)
        {
            Console.WriteLine($"{name}: car {e.Car} is new");
        }
        
    }
}
