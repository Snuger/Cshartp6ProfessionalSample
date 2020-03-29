using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSample
{
    class Program
    {
        static void Main(string[] args)
        {

            //Thread threadA = new Thread(ThreadMethod);     //执行的必须是无返回值的方法 
            //threadA.Name = "小A";
            //Thread threadB = new Thread(ThreadMethod);     //执行的必须是无返回值的方法  
            //threadB.Name = "小B";
            //threadA.Start();
            ////threadA.Join();　    //线程阻塞 
            //threadB.Start();
            ////threadB.Join();

            //Console.ReadKey();



            //Task.Run(() =>                                          //异步开始执行
            //{
            //    Thread.Sleep(1000);                                 //异步执行一些任务
            //    Console.WriteLine("Hello World");                   //异步执行完成标记
            //});
            //Thread.Sleep(1100);                                     //主线程在执行一些任务
            //Console.WriteLine("Main Thread");                       //主线程完成标记
            //Console.ReadLine();

            Say();
            Console.ReadLine();
        }


        private async static void Say()
        {
            var t = TestAsync();
            Thread.Sleep(1100);                                     //主线程在执行一些任务
            Console.WriteLine("Main Thread");                     //主线程完成标记

            Console.WriteLine(await t);                             //await 主线程等待取异步返回结果
        }


        public static void ThreadMethod(object parameter)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("我是:{0},我循环{1}次", Thread.CurrentThread.Name, i);
                Thread.Sleep(300);         //休眠300毫秒              
            }
        }


        static async Task<String> TestAsync() {
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);                                 //异步执行一些任务
                return "Hello World";                               //异步执行完成标记
            });
        }
    }
}
