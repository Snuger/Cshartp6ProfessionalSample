using Amib.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Messaging;

namespace SmartPoolSamples
{
    class Program
    {
        static readonly List<string> buslistQueue = new List<string>() { "BingRenQueue", "MenZhenJZQueue", "MenZhenSFQueue", "MenZhenCFQueue", "ZhuYuanJLQueue", "ZhuYuanSFQueue", "ZhuYuanYZQueue", "JianYanQueue", "JianChaQueue", "JiChuDAQueue", "JiBingGLQueue", "BaoJianGLQueue", "FuNvBJQueue", "ErTongBJQueue", "JiBingKZQueue", "TiJianQueue", "XueYeXXQueue", "QuanYuanRKQueue", "DiaoYueQueue", "QianYueQueue", "YiXueZMQueue", "QuanYuanRKXXqueue", "HeartBeatQueue", "SystemQueue" };
        private static bool running;
        private static IWorkItemsGroup _workItemsGroup;
        static void Main(string[] args)
        {
            //SmartThreadPool smartThreadPool = new SmartThreadPool();
            //smartThreadPool.MaxThreads =1;
            //// 最小线程数,当没有工作项时,线程池最多剩余的线程数
            //smartThreadPool.MinThreads = 0;

            //smartThreadPool.QueueWorkItem(() =>
            //{
            //    int num = 1;
            //    MessageQueue queue = new MessageQueue(@".\private$\osp-error-queue");
            //    queue.ReceiveCompleted += Queue_ReceiveCompleted;
            //    queue.Formatter = new BinaryMessageFormatter();
            //    //queue.BeginReceive(new TimeSpan(0, 0, 2));
            //    while (true)
            //    {
            //        queue.BeginReceive(new TimeSpan(0, 0, 2));
            //        Console.WriteLine($"第{num}传来消息...");
            //        Thread.Sleep(50);
            //        num += 1;
            //    }
            //});


            // foreach (string str in buslistQueue)
            // {
            //     ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
            //     {
            //         int num = 1;
            //         if (MessageQueue.Exists($@".\private$\{obj.ToString()}"))
            //         {
            //             MessageQueue queue = new MessageQueue($@".\private$\{obj.ToString()}");
            //             queue.ReceiveCompleted += Queue_ReceiveCompleted;
            //             queue.Formatter = new BinaryMessageFormatter();
            //             while (true)
            //             {
            //                 queue.BeginReceive(new TimeSpan(0, 0, 2));
            //                 Console.WriteLine($"{obj.ToString()}第{num}传来消息...");
            //                 Thread.Sleep(1000);
            //                 num += 1;
            //             }
            //         }

            //     }), str);
            // }


            //ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
            //{
            //    int num = 1;

            //    foreach (string str in buslistQueue)
            //    {

            //        if (MessageQueue.Exists($@".\private$\{str.ToString()}"))
            //        {
            //            MessageQueue queue = new MessageQueue($@".\private$\{str.ToString()}");
            //            queue.ReceiveCompleted += Queue_ReceiveCompleted;
            //            queue.Formatter = new BinaryMessageFormatter();
            //            while (true)
            //            {
            //                queue.BeginReceive(new TimeSpan(0, 0, 2));
            //                Console.WriteLine($"{str.ToString()}第{num}传来消息...");
            //                Thread.Sleep(2000);
            //                num += 1;
            //            }
            //        }
            //    }
            //}));

            STPStartInfo stpStartInfo = new STPStartInfo();
            stpStartInfo.MaxWorkerThreads=22;
            stpStartInfo.MinWorkerThreads=0;
            stpStartInfo.IdleTimeout = 6*1000;
            SmartThreadPool _smartThreadPool=new SmartThreadPool(stpStartInfo);
             _workItemsGroup=_smartThreadPool;
            Thread workItemsProducerThread=new Thread(new ThreadStart(WorkItemsProducer));
        	workItemsProducerThread.IsBackground = true;
			workItemsProducerThread.Start();

            Console.ReadLine();
        }


        private static void WorkItemsProducer(){
            WorkItemCallback workItemCallback;
            foreach (string str in buslistQueue)
            {
                if (MessageQueue.Exists($@".\private$\{str.ToString()}"))
                {
                    MessageQueue queue = new MessageQueue($@".\private$\{str.ToString()}");
                    queue.ReceiveCompleted += Queue_ReceiveCompleted;
                    queue.Formatter = new BinaryMessageFormatter();
                    while (running)
                    {   workItemCallback = new WorkItemCallback(queue=>DoWork(queue));
                        queue.BeginReceive(new TimeSpan(0, 0, 2));                           
                        Thread.Sleep(2000);
                    }
                }
            }

          
            // while(running){
            //     IWorkItemsGroup workItemsGroup = _workItemsGroup;
            //     if (null == workItemsGroup)
			// 	{
			// 		return;
			// 	}

            // 	try
			// 	{
			// 		workItemCallback = new WorkItemCallback(DoWork);
			// 		workItemsGroup.QueueWorkItem(workItemCallback);
			// 	}
			// 	catch(ObjectDisposedException e)
			// 	{
            //         e.GetHashCode();
			// 		break;
			// 	}				
			// 	Thread.Sleep(Convert.ToInt32(50000));
            // }
           
        }

        private static object DoWork(object obj){
            foreach (string str in buslistQueue)
            {

                if (MessageQueue.Exists($@".\private$\{str.ToString()}"))
                {
                    MessageQueue queue = new MessageQueue($@".\private$\{str.ToString()}");
                    queue.ReceiveCompleted += Queue_ReceiveCompleted;
                    queue.Formatter = new BinaryMessageFormatter();
                    while (true)
                    {
                        queue.BeginReceive(new TimeSpan(0, 0, 2));                           
                        Thread.Sleep(2000);
                    }
                }
            }
            return null;
        }


        private static void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            //bool isReceivedSucceed = true;
            MessageQueue queue = null;
            try
            {
                queue = (MessageQueue)sender;
                System.Messaging.Message msg = queue.EndReceive(e.AsyncResult);
                msg.Formatter = new BinaryMessageFormatter();            
                Console.WriteLine($"{msg.Id,-45}{msg.Label,-30}{"--->",-8}{msg.Body.ToString(),-30}{"ok",-8}");
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                //isReceivedSucceed = false;
            }
            //finally
            //{
            //    if (isReceivedSucceed)
            //    {                   
            //        queue.BeginReceive(new TimeSpan(0, 0, 2));
            //    }
            //}
        }
    }
}
