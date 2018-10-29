using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSampleWithCountdownEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                const int taskCount = 4;
                var mEvents = new CountdownEvent(taskCount);
                var calcs = new Calculator[taskCount];
                for (int t = 0; t < taskCount; t++)
                {
                    int tl = t;
                    calcs[t] = new Calculator(mEvents);
                    Task.Run(() =>
                    {
                        // while (true)
                        // {

                        calcs[tl].Calulation(new Random().Next(1, 1000), new Random().Next(1000, 30000));
                        // }
                    });
                }

                mEvents.Wait(); //等价于Task.WaitAll(Task1,Task2);
                Console.WriteLine("all finished");
                for (int i = 0; i < taskCount; i++)
                {
                    Console.WriteLine($"finished task for {i}, result: {calcs[i].Result}");
                }



            });
            Console.ReadLine();
        }

    }
}