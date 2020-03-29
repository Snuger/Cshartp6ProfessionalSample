using System;
using System.Collections.Generic;
using System.Text;


namespace ReflectionSample
{
   // [Verification(System.DateTime.Now,"修正",Disprition ="学生类",Required =true)]
    public class Student
    {
        private string name;
        private int age;
        private string address;

        public Student(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public string Address { get => address; set => address = value; }
    }
}
