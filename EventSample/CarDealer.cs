using System;
using System.Collections.Generic;
using System.Text;

namespace EventSample
{
    public class CarDealer
    {
        public event EventHandler<CarInfoEventArgs> newCarEventHander;
       
        public void NewCar(string car) {

            Console.WriteLine($"汽车制造商，制造了 {car}");
            newCarEventHander?.Invoke(this, new CarInfoEventArgs(car));

        }
    }
}
