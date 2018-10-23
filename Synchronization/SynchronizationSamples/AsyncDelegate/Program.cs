using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDelegate
{
    class Program
    {
        public delegate int TasksAWhileDelete(int x, int y);

        static void Main(string[] args)
        {
            try
            {
                TasksAWhileDelete delete = TasksAWhile;

                IAsyncResult asyncResult = delete.BeginInvoke(1, 3000, null, null);
            
                while (true)
                {
                    Console.Write(".");                  
                    if (asyncResult.AsyncWaitHandle.WaitOne(50))
                    {
                        Console.WriteLine("\nCan get the result now");
                        break;
                    }                   
                }
                int result = delete.EndInvoke(asyncResult);
                Console.WriteLine($"result:{result}");

            }
            catch (PlatformNotSupportedException)
            {
                Console.WriteLine("PlatformNotSupported exception - with async delegates please use the full .NET Framework");
            }
            Console.ReadLine();
        }


        public static int TasksAWhile(int x, int ms)
        {

            Task.Delay(ms).Wait();
            return 42;
        }
    }
}