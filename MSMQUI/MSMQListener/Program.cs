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

        static void Main(string[] args)
        {           
        
            //Task.Run(()=> {
            //    while (true) {
            mq1 = new MessageQueue(@".\private$\system_request");
            mq1.ReceiveCompleted += new ReceiveCompletedEventHandler(MessageReceived);
            mq1.BeginReceive(new TimeSpan(0, 0, 2));
               // }               

            //}).Wait(6000);

            Console.Read();
 
        }

        


        private static void MessageReceived(object source, ReceiveCompletedEventArgs asyncResult)
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

        static void InvokeMDAO(object state)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} reade state is {state.ToString()}");
            System.Random random = new Random();
            Thread.Sleep(random.Next(1000,3000));
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

        static void InvokeMDAOBit(object obj,object mq) {           
            while (true) {
               

            }

        }
    }
}
