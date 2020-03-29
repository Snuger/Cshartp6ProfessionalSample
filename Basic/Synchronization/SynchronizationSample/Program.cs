using System;
using System.Threading.Tasks;

namespace SynchronizationSample
{
    class Program
    {
        static void Main(string[] args)
        {            
            int numTasks = 20;
            var state = new ShareState();
            var tasks = new Task[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                tasks[i] = Task.Run(() => new Job(state).DoTheJob());
            }

            Task.WaitAll(tasks);
            Console.WriteLine($" sumarized {state.State}");
            Console.ReadLine();
        }
    }
}
