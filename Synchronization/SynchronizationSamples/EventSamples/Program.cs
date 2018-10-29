using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => {
                const int taskCount = 4;
                var mEvents = new ManualResetEventSlim[taskCount];
                var waitHandles = new WaitHandle[taskCount];
                var calcs = new Calculator[taskCount];

                for (int t = 0; t < taskCount; t++)
                {
                    int tl = t;
                    mEvents[t] = new ManualResetEventSlim(false);
                    waitHandles[t] = mEvents[t].WaitHandle;
                    calcs[t] = new Calculator(mEvents[t]);
                    Task.Run(() =>
                    {
                       while (true) {
                            
                            calcs[tl].Calulation(new Random().Next(1,1000), new Random().Next(1000, 30000));
                        }                       
                    });

                }

                while (true)
                {
                    int index = WaitHandle.WaitAny(waitHandles);
                    if (index == WaitHandle.WaitTimeout)
                    {
                        Console.WriteLine("time out");
                    }
                    else
                    {
                        mEvents[index].Reset();
                        Console.WriteLine($"finished task for {index}, result: {calcs[index].Result}");
                    }
                }
            });
            Console.ReadLine();
        }

    }
}
