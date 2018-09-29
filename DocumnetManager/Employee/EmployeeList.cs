using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager
{
    public class EmployeeList<TEmployee> where TEmployee : Employee, IEmployee, IComparable<TEmployee>, new()
    {
        public EmployeeList()
        {
        }
    }
}
