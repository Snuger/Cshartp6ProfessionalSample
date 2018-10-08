using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ReflectionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            dynamic arg;
            arg = "你还好吗？";
            Console.WriteLine(arg.GetType());

            arg = 20;
            Console.WriteLine(arg.ToString());

            string ag = "你可曾想起过我...";
            Console.WriteLine(ag.GetType());

            dynamic student = new Student("lilei",29,"河南省内乡县");
            Console.WriteLine(student.GetType());

            Console.WriteLine(student.Name);


            dynamic dynamic = new WroxDynamicObject();
            dynamic.FirstName = "sb";
            dynamic.LastName = "wcwgmmy";

            Console.WriteLine(dynamic.GetType());
            Console.WriteLine($"{dynamic.FirstName}{dynamic.LastName}，转眼间，我们都已经到了而立之年，你是否还会想起那个他...");
            Console.ReadLine();


            dynamic expObj = new ExpandoObject();
            expObj.FirstName = "li";
            expObj.LastName = "junli";
            Console.WriteLine($"{expObj.FirstName} {expObj.LastName}");
            Console.ReadLine();

            Func<DateTime, string> GetTommorrow = today => today.AddDays(1).ToString("d");
            expObj.GetTommorrowDate = GetTommorrow;
            Console.WriteLine($"tommorrow is {expObj.GetTommorrowDate(DateTime.Now)}");
            expObj.Friends = new List<Student>();
            expObj.Friends.Add(new Student("李俊丽",30,"河南南阳"));



        }
    }
}
