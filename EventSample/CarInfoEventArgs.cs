using System;
using System.Collections.Generic;
using System.Text;

namespace EventSample
{
    public class CarInfoEventArgs:EventArgs
    {
        public CarInfoEventArgs(string car)
        {
            Car = car;
        }

        public string Car { get; }
    }
}
