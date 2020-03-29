using System;

namespace GenericesMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int x = 5, y = 10;
            Swap(ref x, ref y);
            Console.WriteLine(y);
            Console.ReadLine();
        }

        public static void Swap<T>(ref T x, ref T y) {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }
    }
}
