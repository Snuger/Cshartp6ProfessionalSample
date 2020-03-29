using System;
using System.Threading;

namespace InterlockedExchange_Example
{
    class Program
    {

        private static int usingResource = 0;

        private const int numThreadIterations = 5;
        private const int numThreads = 10;

        static void Main(string[] args)
        {
            Thread myThread;

            Random rnd = new Random();

            for (int i = 0; i < numThreads; i++)
            {
                myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);

                //Wait a random amount of time before starting next thread.
                Thread.Sleep(rnd.Next(0, 1000));
                myThread.Start();
            }


            Console.ReadLine();        }
        private static void MyThreadProc()
        {
            for (int i = 0; i < numThreadIterations; i++)
            {
                //UseResource();
                 Interlocked.Add(ref usingResource, i);
               // usingResource += i;
                //Wait 1 second before next attempt.
                Thread.Sleep(1000);

                Console.WriteLine($" thread {Thread.CurrentThread.Name}   result:{usingResource}");
            }
        }


        static bool UseResource()
        {
            //0 indicates that the method is not in use.
           
             if (0 == Interlocked.Exchange(ref usingResource, 1))
            {
                Console.WriteLine($"{usingResource}");

                Console.WriteLine("{0} acquired the lock", Thread.CurrentThread.Name);

               
                //Code to access a resource that is not thread safe would go here.

                //Simulate some work
                Thread.Sleep(500);

                Console.WriteLine("{0} exiting lock", Thread.CurrentThread.Name);
                
                //Release the lock
                Interlocked.Exchange(ref usingResource, 0);


                Console.WriteLine($"{usingResource}");


                return true;
            }
            else
            {
                Console.WriteLine("   {0} was denied the lock", Thread.CurrentThread.Name);
                return false;
            }
            
             
        }

    }


}
