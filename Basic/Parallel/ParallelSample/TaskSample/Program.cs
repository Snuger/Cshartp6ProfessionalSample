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
                    case "-r":
                        //TaskWithResultDemo();
                        TaskWithResultDemo2();
                        break;
                    case "-c":
                        ContinuationTasks();
                        break;
                    case "-pc":
                        ParentAndChild();
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


        private static void TaskWithResultDemo()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create(8, 3));
            t1.Start();
           // Console.WriteLine(t1.Result);
            //t1.Wait();
            Console.WriteLine($"result form task: one is  {t1.Result.Item1} two is  {t1.Result.Item2} ");

        }


        private static void TaskWithResultDemo2() {
            var task = new Task<Tuple<int, int>>((object division) =>
            {
                Tuple<int, int> div = (Tuple<int, int>)division;
                int result = div.Item1 / div.Item2;
                int reminder = div.Item1 % div.Item2;
                Console.WriteLine("Task creates a result...");
                return Tuple.Create(result, reminder);
            },Tuple.Create(8, 3));

            task.Start();

            Console.WriteLine($"result form task: one is  {task.Result.Item1} two is  {task.Result.Item2} ");

        }

        private static Tuple<int, int> TaskWithResult(object division)
        {
            Tuple<int, int> div = (Tuple<int, int>)division;

            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            Console.WriteLine("Task creates a result...");
            return Tuple.Create(result, reminder);
        }



        private static void ContinuationTasks()
        {
            var task = new Task(DoFirt);
            Task t2=task.ContinueWith(DoSecond);
            task.Start();
        }
        private static void DoFirt() {
            Console.WriteLine($"doing some task {Task.CurrentId}");
            Task.Delay(3000).Wait();
        }


        private static void DoSecond(Task t)
        {
            Console.WriteLine($"task {t.Id} finished");
            Console.WriteLine($"this task id {Task.CurrentId}");
            Console.WriteLine("do some cleanup");
            Task.Delay(3000).Wait();
        }

        private static void ContinuationTasks1()
        {
            Task t1 = new Task(() => {
                Console.WriteLine($"doing some task {Task.CurrentId}");
                Task.Delay(3000).Wait();
            });

            t1.ContinueWith((Task task) =>
            {
                Console.WriteLine($"task {task.Id} finished");
                Console.WriteLine($"this task id {Task.CurrentId}");
                Console.WriteLine("do some cleanup");
                Task.Delay(3000).Wait();
            });

        }


        private static void ParentAndChild() {
            Task parent = new Task(ParentPack);
            parent.Start();
            Task.Delay(2000).Wait();
            Console.WriteLine(parent.Status);
            Task.Delay(4000).Wait();
            Console.WriteLine(parent.Status);

        }

        private static void ParentPack() {
            Console.WriteLine($"task id {Task.CurrentId}");
            var task = new Task(ChildPack);
            task.Start();
            Task.Delay(1000).Wait();
            Console.WriteLine("parent started child");

        }

        private static void ChildPack() {
            Console.WriteLine("child");
            Task.Delay(3000).Wait();
            Console.WriteLine("child finished");
        }
    }
}
