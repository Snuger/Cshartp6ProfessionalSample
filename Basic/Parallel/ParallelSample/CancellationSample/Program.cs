using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //CancelParallerFor();

            CancelTask();

            Console.ReadLine();
        }


        private static void CancelParallerFor() {

            var cts = new CancellationTokenSource();
            cts.Token.Register(()=> { Console.WriteLine("***  token canncelled"); });

            cts.CancelAfter(500);

            try
            {
                ParallelLoopResult loopResult =
                     Parallel.For(0, 100, new ParallelOptions
                     { CancellationToken = cts.Token }, 
                     x =>
                     {
                         Console.WriteLine($" loop {x} started");
                         int sum = 0;
                         for (int i = 0; i < 100; i++) {
                             Task.Delay(2).Wait();
                             sum += i;
                         }
                         Console.WriteLine($"loop {x} finished");
                     });

            }
            catch (OperationCanceledException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private static void CancelTask() {

            var cts = new CancellationTokenSource();
            cts.Token.Register(()=> {
                Console.WriteLine("**** task started ");
            });

            cts.CancelAfter(5000);

            Task task = new Task(()=> {
                int x = 0;
                bool stop = false;

                while (!stop) {

                    Console.WriteLine($" {x} times in task");
                    Task.Delay(1000).Wait();
                    CancellationToken token = cts.Token;

                    if (token.IsCancellationRequested) {
                        Console.WriteLine($"Cancelling was requested  canclling from  with in the task");
                       // token.ThrowIfCancellationRequested();
                        stop = true;
                    }
                    x += 1;
                }
                Console.WriteLine("task finished without cancellation...");
            },cts.Token);

            try
            {
                task.Start();
                task.Wait();
            }
            catch (AggregateException ex)
            {

                Console.WriteLine($"exception: {ex.GetType().Name}, {ex.Message}");
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"inner exception: {ex.InnerException.GetType()}," +
                      $"{ex.InnerException.Message}");
                }
            }

        }
    }
}
