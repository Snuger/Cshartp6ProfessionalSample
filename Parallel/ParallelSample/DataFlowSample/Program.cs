using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowSample
{
    class Program
    {

        /*
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var processIpput = new ActionBlock<string>(str=>{
                Console.WriteLine($" user input: {str}");
            });

            bool exit = false;

            while(!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else {
                    processIpput.Post(input);
                }
            }
        }
        */

        private static BufferBlock<string> s_buffer = new BufferBlock<string>();

        static void Main(string[] args) {

            Task t1 = Task.Run(()=>Producter());
            Task t2 = Task.Run(async()=> await ConsumerAsync());
            Task.WaitAll(t1,t2);
        }


        private static void Producter() {
            bool exit = false;
            while (!exit) {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else {
                    s_buffer.Post(input);
                }
            }

        }

        public static async Task ConsumerAsync() {
            while (true) {
                string data = await s_buffer.ReceiveAsync();
                Console.WriteLine($"user input data:{data}");
            }
        }
    }
}
