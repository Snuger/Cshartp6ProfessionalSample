using System;
using System.Collections.Generic;
using System.Text;

namespace MulitcastDelegets
{
  public   class MulitcastOptions
    {
      public  static void MultiplyByTwo(double value)
        {
            double reuslt = value * 2;
            Console.WriteLine($"倍率计算，结果如下:{reuslt}");
        }

       public  static void Square(double value) {
            double result = value * value;
            Console.WriteLine($"Squaring: {value} gives {result}");
        }



        public double CompareRide(double a, double b) => a * b;

        public double CompareAdd(double a, double b) => a + b;


        public void CompareAdd(double a, double b, ref double c) => c = a + b;


        public void CompareReduce(double a, double b, ref double c) => c = a - b;

    }
}
