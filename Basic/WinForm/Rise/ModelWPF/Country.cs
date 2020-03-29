using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWPF
{
    public sealed class Country : ICountry
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public override string ToString() => Name;
    }
}
