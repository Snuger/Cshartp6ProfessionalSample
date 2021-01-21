using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBuilderDesigin
{
    class Manufacturer
    {
        private readonly CarBuilder _carBuilder;

       public Manufacturer(CarBuilder carBuilder)
        {
            _carBuilder = carBuilder;
        }

        public Car Delivery() => _carBuilder.Delivery();
    }
}
