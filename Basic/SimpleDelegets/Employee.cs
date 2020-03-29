using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDelegets
{
   public  class Employee
    {
        public Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        public string Name { get; }

        public decimal Salary { get; private set; }

        public override string ToString() => $"{Name} {Salary :C}";


        public static bool CompareSalary(Employee e1, Employee e2) => e1.Salary > e2.Salary;



        public bool CompareSalaryPro(Employee e1, Employee e2) => e1.Salary > e2.Salary;
    }
}
