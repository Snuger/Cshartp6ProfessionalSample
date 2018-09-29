using System;
using System.Collections.Generic;
using System.Text;

namespace ArraySample
{
  public   class Persion
    {
        public Persion()
        {
        }

        public Persion(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString() => $"FirstName:{FirstName},LastName:{LastName},FullName:{FirstName}-{LastName}";


    }
}
