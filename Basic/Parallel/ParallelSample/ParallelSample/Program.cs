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


            /*
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
                */

            //ParallerForWithInit();

            //ParallerForEach();

            ParallerForEachBrack();

            //测试调用多个方法
            ParallerInvok();

            Console.ReadLine();


        }
            
        private static void Log(string prefix) {
            Console.WriteLine($"{prefix},task:{Task.CurrentId}, Thread：{Thread.CurrentThread.ManagedThreadId}");
        }

        private static void ParallerForWithInit() {
            Parallel.For<string>(0, 10, () =>
            {
                Log($"init thread");
                return $"t {Thread.CurrentThread.ManagedThreadId}";

            },(i,pls,str1)=> {
                Log($" body {i} str1 {str1}");
                return str1;
            },(str1)=> {
                Log($"finally {str1}");
            });

        }

        private static void ParallerForEach() {
            string[] data = {"Zero","one","two","three","four","five","six","seven","eight","ninie","ten","eleven","tweive" };

            ParallelLoopResult result = Parallel.ForEach<string>(data, str => {
                Console.WriteLine(str);
            });


        }

        private static void ParallerForEachBrack() {
            string[] data = { "Zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "ninie", "ten", "eleven", "tweive" };
            ParallelLoopResult result=
                Parallel.ForEach<string>(data,(string item,ParallelLoopState state)=> {
                    if (item == "five")
                        state.Break();
                    Console.WriteLine(item);
                });

        }

        private static void ParallerInvok() {
           Parallel.Invoke(Aoo,Foo,Boo);
        }




        private static void Foo() {
            Console.WriteLine("foo");
        }

        private static void Boo() {
            Console.WriteLine("Boo");
        }

        private static void Aoo()
        {
            Console.WriteLine("Aoo");
        }



        
    }
}
