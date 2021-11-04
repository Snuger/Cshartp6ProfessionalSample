using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternDesigin
{
    internal class Person
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string  MaritalStatus { get; set; }

        public Person(string name, string gender, string maritalStatus)
        {
            Name = name;
            Gender = gender;
            MaritalStatus = maritalStatus;
        }

        public int GetAge(string name)=>new Random().Next(20, 100);


       public string GetGender()=>this.Gender;
    }
}
