using System;
using System.Threading;


namespace ThreadSample
{
    class Program
    {
        static void Main(string[] args)
        {            

            Thread threadA = new Thread(ThreadMethod);     //执行的必须是无返回值的方法 
            threadA.Name = "小A";
            Thread threadB = new Thread(ThreadMethod);     //执行的必须是无返回值的方法  
            threadB.Name = "小B";
            threadA.Start();
            //threadA.Join();　    //线程阻塞 
            threadB.Start();
            //threadB.Join();

            Console.ReadKey();
        }


        public static void ThreadMethod(object parameter)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("我是:{0},我循环{1}次", Thread.CurrentThread.Name, i);
                Thread.Sleep(300);         //休眠300毫秒              
            }
        }
    }
}
