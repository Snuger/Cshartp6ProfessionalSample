using System;
using System.Collections.Generic;
using System.Text;

namespace EventSample
{
   public  class Consumer
    {
        public Consumer(string name)
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
