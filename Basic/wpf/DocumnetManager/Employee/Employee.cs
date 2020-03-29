using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}
