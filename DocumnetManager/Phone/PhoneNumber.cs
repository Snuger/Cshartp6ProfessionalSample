using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager.Phone
{
    public class PhoneNumber
    {
        private string name;
        private string number;

        public PhoneNumber(string number, string name)
        {
            Number = number;
            Name = name;
        }

        public string Number { get => number; set => number = value; }
        public string Name { get => name; set => name = value; }
    }
}
