using System;
using System.Threading.Tasks;

namespace ThreadingIssues
{
    class Program
    {
        static void Main(string[] args)
        {
           
            StateObject state = new StateObject();
            for (int i = 0; i < 2; i++) {
                Task.Run(() => new SampleTask().RaceCondition(state));
            }

            Console.ReadLine();
            
        }
    }
}
