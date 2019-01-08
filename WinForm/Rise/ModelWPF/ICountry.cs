using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWPF
{
   public  interface ICountry
    {
        string ImagePath { get; set; }
        string Name { get; set; }

        string ToString();
    }
}
