using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSample
{
    class Program
    {

        private static object s_logLock = new object();

        static void Main(string[] args)
        {
            bool stop = false;
            ShowUsage();
            string input = string.Empty;
            while (!stop) {
                input = Console.ReadLine();
                switch (input) {
                    case "-p":
                        TaskUsingThreadPool();
                        break;
                    case "-s":
                        RunSynchronousTask();
                        break;
                    case "-l":
                        LongRuningTask();
                        break;
                    case "-q":
                        stop = true;
                        break;
                    case "-h":
                         ShowUsage();
                        break;
                }
            }            
        }

        private static void ShowUsage()
        {
            Console.WriteLine("TaskSamples option");
            Console.WriteLine("options");
            Console.WriteLine("\t-p\tUse Thread Pool");
            Console.WriteLine("\t-s\tUse Synchronous Task");
            Console.WriteLine("\t-l\tUse Long Running Task");
            Console.WriteLine("\t-r\tTask with Result");
            Console.WriteLine("\t-c\tContinuation Tasks");
            Console.WriteLine("\t-pc\tParent and Child");
            Console.WriteLine("\t-q\tquite");
            Console.WriteLine("\t-h\thelp");
        }

        private static void TaskUsingThreadPool() {
            TaskFactory factory = new TaskFactory();
            Task t1 = factory.StartNew(TaskMethod, "using a task factory");
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");
            Task t3 = new Task(TaskMethod, "using a task constructor and start");
            t3.Start();
            Task t4 = Task.Run(()=> {
                TaskMethod("using the run method");
            });
        }

        private static void TaskMethod(object obj) {
            Log(obj?.ToString());
        }

        private static void Log(string title) {
            lock (s_logLock) {
                Console.WriteLine(title);
                Console.WriteLine($"task id:{Task.CurrentId?.ToString()??"no task"}, thread:{Thread.CurrentThread.ManagedThreadId}");
#if (NET64)
                     Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
#endif
                Console.WriteLine($"is background thread:{Thread.CurrentThread.IsBackground}");
            }
        }

        private static void RunSynchronousTask() {
            Log("just the main thread");
            Task task = new Task(TaskMethod, "run sync");
            task.RunSynchronously();
        }

        private static void LongRuningTask() {
            Task task = new Task(TaskMethod, "Long running task",TaskCreationOptions.LongRunning);
            task.Start();          
        }

    }
}
