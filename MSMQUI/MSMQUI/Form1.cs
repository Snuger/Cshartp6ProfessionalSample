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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
using System.Threading;

namespace MSMQUI
{
    public partial class Form1 : Form
    {
        private MessageQueue mq;
        private System.Messaging.Message msg;
 
        public Form1()
        {
            InitializeComponent();

            msg = new System.Messaging.Message();
            msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (MessageQueue.Exists(mqCreateTxt.Text))
                //creates an instance MessageQueue, which points 
                //to the already existing MyQueue
                mq = new System.Messaging.MessageQueue(mqCreateTxt.Text);
            else
                //creates a new private queue called MyQueue 
                mq = MessageQueue.Create(mqCreateTxt.Text);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(countTxt.Text);
        

            while (count > 0)
            {
                System.Messaging.Message message = new System.Messaging.Message();
                message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                message.Body = $"msg on {count.ToString()}";

                ThreadPool.QueueUserWorkItem(new WaitCallback(SendMessage), message);
                count--;
            }
        }

        private void SendMessage(object item)
        {
            MessageQueue queue;
            if (MessageQueue.Exists(mqCreateTxt.Text))
            {
                queue = new System.Messaging.MessageQueue(mqCreateTxt.Text);
            }
            else
            {
                queue = MessageQueue.Create(mqCreateTxt.Text);
            }

            System.Messaging.Message messageItem = item as System.Messaging.Message;
            if (queue != null)
            {
                try
                {
                    queue.Send(messageItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }  
}
