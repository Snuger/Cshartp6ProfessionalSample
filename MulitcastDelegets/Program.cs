using System;

namespace MulitcastDelegets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Action<double> action = MulitcastOptions.MultiplyByTwo;
            action += MulitcastOptions.Square;

            ProcessAndDisplayNumber(action, 9);
            ProcessAndDisplayNumber(action, 12);
            ProcessAndDisplayNumber(action, 11);
            ProcessAndDisplayNumber(action, 8);
            ProcessAndDisplayNumber(action, 112);

            Console.WriteLine("多波委托执行完成！");

            Console.WriteLine($"第一波");
            MulitcastOptions mulitcastOptions = new MulitcastOptions();
            Func<double, double, double> func = mulitcastOptions.CompareRide;
            func += mulitcastOptions.CompareAdd;

            ProcessAndDisplayNumberPro(func, 11, 11);
            ProcessAndDisplayNumberPro(func, 12, 12);
            ProcessAndDisplayNumberPro(func, 13, 13);
            ProcessAndDisplayNumberPro(func, 14, 14);


            Console.WriteLine($"第二波");

            ProcessAndDisplayNumberPro(new MulitcastOptions().CompareRide, 23.3, 33.23);


            Console.ReadLine();



            double result = 0;


     

            ProcessAddRefCompare(mulitcastOptions.CompareAdd, 33, 44, ref result);
            Console.WriteLine($"ref 返回结果:{result}");

            Console.ReadLine();



        }

        public static void ProcessAndDisplayNumber(Action<double> action, double a) {

            Console.WriteLine();
            Console.WriteLine($"ProcessAndDisplayNumber called with value = {a}");
            action(a);
        }



        public static void ProcessAndDisplayNumberPro(Func<double, double, double> action, double a, double b) {     
           double result= action(a, b);
            Console.WriteLine($"{a} * {b} 计算结果如下  :{result}");
        }



        public static void ProcessAddRefCompare(Func<double, double, double> func, double a, double b, ref double c) {
            c = func(a, b);          
        }
    }
}
