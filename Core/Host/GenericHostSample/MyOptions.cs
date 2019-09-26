using System;
using System.Collections.Generic;
using System.Text;

namespace GenericHostSample
{
    public class MyOptions
    {
        public MyOptions(string optinos1)
        {
            Optinos1 = optinos1;
        }

        public string Optinos1 { get; set; }

        public int Options2 { get; set; } = 5;

    }
}
