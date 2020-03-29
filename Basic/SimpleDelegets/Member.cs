using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDelegets
{
   public  class Member<TEmployee> where TEmployee:Employee 
    {
        public Member(IList<TEmployee> employees)
        {
            Employees = employees;
        }
        public IList<TEmployee> Employees { get; private set; }

        public bool CompareSalaryPro(TEmployee e1, TEmployee e2) => e1.Salary > e2.Salary;
    }
}
