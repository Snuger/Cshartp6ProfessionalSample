using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Messaging;

namespace MessageQueue.Listener
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("命令提示:启动侦听-start!");
            string _commond = string.Empty;
            bool stop = false;
            while (!stop)
            {
                _commond = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(_commond))
                {
                    switch (_commond)
                    {
                        case "exit":

                            break;
                        case "start":
                            OnStart();
                            break;
                        default:
                            break;
                    }

                }

            }
        }

        private async static void OnStart()
        {
            //https://blog.csdn.net/huiwuhuiwu/article/details/53580335
            List<string> queus = new List<string>() { "system_request", "system_request2" };
            while (true) {
                //queus.ForEach(async (item) => {

                //});

                Task<String> task = GetMessage(queus[0]);
                Task<String> task1 = GetMessage(queus[1]);

                //await Task.WhenAny(new Task[] { task,task1 });
                var completedTask = Task.WhenAny(new Task[] { task, task1 }).Result;
              //  Console.WriteLine(completedTask.r);
              
               // ThreadPool.QueueUserWorkItem(new WaitCallback(InvokeMDAO),completedTask.re);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(InvokeMDAO), $"从队列[{queus[1]}]中读取到消息:{(await task1).ToString()}");
                // Task.Delay(100);
            }
        } 


        //private async Func<List<string>, object> GetBackMessage (List<string> queus){
        //    foreach (string s in queus) {
        //        Task<string> task = GetMessage(s);
        //        return await task;
        //    }
        //}

        static void InvokeMDAO(object state)
        {
            //if (state.ToString() == "no message")
            //{

            //}
            //else {
            //    Console.WriteLine($"线程  {Thread.CurrentThread.ManagedThreadId} 读取到消息 {state.ToString()}");
            //   // System.Random random = new Random();
            //   // Thread.Sleep(random.Next(1000, 3000));
            //}    

            Console.WriteLine($"线程 {Thread.CurrentThread.ManagedThreadId} {state.ToString()}");
            System.Random random = new Random();
             Thread.Sleep(random.Next(100, 3000));
        }


        private static async Task<string> GetMessage(string queue) {
            return await Task.Run(()=> 
            {
                try
                {
                    string path =@".\private$\" + queue;
                    System.Messaging.MessageQueue messageQueue = null;
                    if (System.Messaging.MessageQueue.Exists(path))
                        messageQueue = new System.Messaging.MessageQueue(path);
                    else                        
                        messageQueue = System.Messaging.MessageQueue.Create(path);                    
                    Message message = messageQueue.Receive(new TimeSpan(0, 0, 2));
                    message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                    return $"队列名称:{queue} 消息内容 {message.Body.ToString()}";

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }                         
              
            });

        }



        private static async Task<String> GetMessage()
        {
            List<string> queus = new List<string>() { "system_request", "system_request2" };

            queus.ForEach((item)=> {

                Task.Run(() =>
                {
                    try
                    {
                        System.Messaging.MessageQueue messageQueue = new System.Messaging.MessageQueue(@".\private$\" + item);
                        Message message = messageQueue.Receive(new TimeSpan(0, 0, 2));
                        message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                        return message.Body.ToString();

                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }

                });

            });

            return await Task.Run(() =>
            {
                try
                {                 


                    System.Messaging.MessageQueue messageQueue = new System.Messaging.MessageQueue(@".\private$\" + queus[0]);
                    Message message = messageQueue.Receive(new TimeSpan(0, 0, 2));
                    message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                    return message.Body.ToString();

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            });

        }

    }
}

