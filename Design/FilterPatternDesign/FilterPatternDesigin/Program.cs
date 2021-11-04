using System;
using System.Collections.Generic;

namespace FilterPatternDesigin
{
    internal class Program
    {
        static void Main(string[] args)
        {
           List<Person> persons = new List<Person>();
            persons.Add(new Person("Robert", "Male", "Single"));
            persons.Add(new Person("John", "Male", "Married"));
            persons.Add(new Person("Laura", "Female", "Married"));
            persons.Add(new Person("Diana", "Female", "Single"));
            persons.Add(new Person("Mike", "Male", "Single"));
            persons.Add(new Person("Bobby", "Male", "Single"));

            ICriteria maleCriteria = new MaleCriteria();          
            PrintPersons(maleCriteria.MeetCriteria(persons));
            ICriteria femaleCriteria = new FemaleCriteria();
            ICriteria andCriteria=new AndCriteria(maleCriteria,femaleCriteria);
            //PrintPersons(andCriteria.MeetCriteria(persons));
            //ICriteria orCriteria=new OrCriteria(maleCriteria, femaleCriteria);
            //PrintPersons(orCriteria.MeetCriteria(persons));
            Console.ReadLine();
        }

        public static void PrintPersons(List<Person> persions)
        {
            foreach (var item in persions)
            {
                Console.WriteLine($"{item.Name},{item.Gender},{item.MaritalStatus}");
            }
        }
    }
}
