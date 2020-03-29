/*************************************
/* Copyright (c) 2012 Daniel Dong
 * 
 * Author：Daniel Dong
 * Blog：  www.cnblogs.com/danielWise
 * Email： guofoo@163.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace MSMQListener
{
    class Program
    {
        static MessageQueue mq1;
        private static readonly string[] queueNames = { "queue1", "queue2", "queue3", "queue4", "queue5", "queue6", "queue7", "queue8", "queue9", "queue10" };

        static void Main(string[] args)
        {
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
                            Console.WriteLine("准备开启服务...");
                            onStart();
                            break;
                        case "init":
                            break;
                        default:
                            break;
                    }
                }

            }

            Console.Read();

        }

        private static void onStart()
        {      
            foreach (string s in queueNames)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(InvokeMDAO), s);
            }
        }



        private static void MessageReceived(object source, ReceiveCompletedEventArgs asyncResult)
        {
            bool isReceivedSucceed = true;
            MessageQueue mq = null;
            try
            {
                mq = (MessageQueue)source;
                Message m = mq.EndReceive(asyncResult.AsyncResult);
                m.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                System.Console.WriteLine($"{m.Body.ToString()}");
                //处理其他事情     

            }
            catch (MessageQueueException)
            {
                isReceivedSucceed = false;
            }
            catch (Exception ex)
            {
                isReceivedSucceed = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //处理正常的情况下，读取下一条
                if (isReceivedSucceed)
                {
                    mq1.BeginReceive(new TimeSpan(0, 0, 2));
                }
            }
        }

        static void InvokeMDAO(object state)
        {
            MessageQueue queue = new MessageQueue($@".\private$\{state.ToString()}");
            queue.ReceiveCompleted += new ReceiveCompletedEventHandler(MessageReceived);
            while (true)
            {
                queue.BeginReceive(new TimeSpan(0, 0, 2));
                Thread.Sleep(5000); 
            }

        }

        private static void MessageReceived2(object source, ReceiveCompletedEventArgs asyncResult)
        {

            bool isReceivedSucceed = true;
            MessageQueue mq = null;

            try
            {
                //Connect to the queue
                mq = (MessageQueue)source;
                Message m = mq.EndReceive(asyncResult.AsyncResult);
                m.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });

                ThreadPool.QueueUserWorkItem(new WaitCallback(InvokeMDAO), m.Body);
            }
            catch (MessageQueueException)
            {
                isReceivedSucceed = false;
            }
            catch (Exception ex)
            {
                isReceivedSucceed = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (isReceivedSucceed)
                {
                    mq1.BeginReceive(new TimeSpan(0, 0, 2));
                }
            }
        }

    }
}
