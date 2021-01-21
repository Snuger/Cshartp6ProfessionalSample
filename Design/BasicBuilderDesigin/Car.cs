using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBuilderDesigin
{
    class Car
    {
        public string Brand { get; set; }

        public Engine Engine { get; set; }

        public Wheel[] Wheels { get; set; } = new Wheel[4];
    }
}
