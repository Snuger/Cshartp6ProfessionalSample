using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //ParallelLoopResult result =
            //    Parallel.For(0, 20, async i =>
            //    {
            //        Log($"S{i}");
            //        await Task.Delay(10);
            //        Log($"e{i}");
            //    });
            //Console.WriteLine($"is completed:{result.IsCompleted}");
            //Console.ReadLine();



            ParallelLoopResult loopResult =
                Parallel.For(10, 40, (int i, ParallelLoopState pls) =>
                {
                    Log($"s{i}");
                    if (i > 12) {
                        pls.Break();
                        Log($"break now ....{i}");
                    }
                    Task.Delay(10).Wait();
                    Log($"e{i}");
                });
            Console.ReadLine();
        }
            
        private static void Log(string prefix) {
            Console.WriteLine($"{prefix},task:{Task.CurrentId}, Thread：{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
