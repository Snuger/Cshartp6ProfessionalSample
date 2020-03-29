using System;

namespace AnonymousDelegets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string mid = ",middle part,";

            Func<string,string> func=delegate(string parmes)
            {
                parmes += mid;
                parmes += "this is add string...";
                return parmes;
            };

            Console.WriteLine(func("start string"));
            Console.ReadLine();





            Func<string, string> lamda = (parmes) =>
            {
                parmes += mid;
                parmes += "this is add string...";
                return parmes;
            };

            Console.WriteLine(lamda("dfdaf string"));
            Console.ReadLine();


            Func<int, int, int> leamda = (a, b) =>
            {
                return a + b;
            };
            Console.WriteLine(leamda(12, 23));         
            Console.ReadLine();


            Func<double, double, double> twoParmeras = (double a, double b) => a * b;
            Console.WriteLine(twoParmeras(12, 23.9));

            Func<double, double> threadParmes = (double x) => x * x;

            Func<double, double, double> fourThead = (double x, double y) =>
            {
                return x * y;
            };



            int someVal = 5;
            Func<int, int> compare = x => x + someVal;
            Console.WriteLine(compare(10));
            Console.ReadLine();


            Console.WriteLine(lamda("测试闭包！"));
            AnonymousClass cls = new AnonymousClass(10);
            Func<int, int> Five = (x) => cls.AnonymousMethod(x);
            Console.WriteLine(Five(10));

            Console.ReadLine();






        }
    }
}
