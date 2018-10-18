using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.IO;
using System.Collections.Generic;

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

            // Task t1 = Task.Run(()=>Producter());
            // Task t2 = Task.Run(async()=> await ConsumerAsync());
            //Task.WaitAll(t1,t2);

            var target = setupPipeline();
            target.Post(@"E:\Code Studio\C#-Project\Cshartp6ProfessionalSample\Parallel\ParallelSample\DataFlowSample");
            Console.ReadLine();
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



        private static IEnumerable<string> GetFileNames(string path) {
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs")) {
                yield return fileName;
            }
        }

        private static IEnumerable<string> LoadLines(IEnumerable<string> fileNames) {
            foreach (var fileName in fileNames) {
                using (FileStream stream = File.OpenRead(fileName)) {
                    var reader = new StreamReader(stream);
                    string line = string.Empty;
                    while ((line=reader.ReadLine()) != null) {
                        yield return line;
                    }
                }

            }

        }

        private static IEnumerable<string> GetWords(IEnumerable<string> lines) {

            foreach (string line in lines) {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (string word in words)
                {
                    if (!string.IsNullOrEmpty(word))                    
                        yield return word;                   

                }
            }
        }

        private static ITargetBlock<string> setupPipeline() {
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(path=> {
                return GetFileNames(path);
            });


            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(fileNames=> {
                 return LoadLines(fileNames);
            });


           // var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(lines1=> {
           //      return GetWords(lines1);
           // });

            var dispplay = new ActionBlock<IEnumerable<string>>(lines2=> {
                foreach (string s in lines2) {
                    Console.WriteLine(s);
                }
            });

            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(dispplay);
           // lines.LinkTo(words);
           // words.LinkTo(dispplay);
            return fileNamesForPath;
        }
    }
}
