using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager.Constructor
{
    public class PostalFund
    {
        public PostalFund()
        {
        }

        public PostalFund(string name, string no)
        {
            Name = name;
            No = no;
        }

        public string Name { get; set; }

        public string No { get; set; }

    }
}
