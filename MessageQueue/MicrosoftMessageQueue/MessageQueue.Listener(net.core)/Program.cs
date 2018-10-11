using System;
using System.Timers;
using System.Threading;


namespace MessageQueue.Listener
{
    class Program
    {
        System.Timers.Timer timer1;  //计时器
        private bool IsStatus = true;

        private Thread PuschThread;

        /// <summary>
        /// 定时监控扫描时间
        /// </summary>
        private int IntervalTime = AppConfig.IntervalTime * 60000;
        /// <summary>
        /// N分钟无状态回复则转短信
        /// </summary>
        private int PushSmsMinute = AppConfig.PushSmsMinute;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string _commond = string.Empty;
            bool stop = false;
            while (!stop) {
                _commond = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(_commond))
                {
                    switch (_commond)
                    {
                        case "exit":

                            break;
                        case "start":

                            break;
                        default:
                            break;
                    }

                }
                   
            }      
        }

        private void OnStart() {

            timer1 = new System.Timers.Timer();
            timer1.Interval = IntervalTime;  //设置计时器事件间隔执行时间默认15分钟执行一次
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;

            //监听
            IsStatus = true;
            PuschThread = new Thread(new ThreadStart(RecevieMessage));
            PuschThread.IsBackground = true;
            PuschThread.Start();
            Console.WriteLine("家校宝消息监控服务开始执行！");

        }


        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var list = PushToMessage.get_pushtosms(AppConfig.PushSmsMinute);
                int MessageCount = list.Count;
                Common.WriteToLogFile(string.Format("共有{0}条已超出{1}分钟未阅读消息数据", MessageCount, PushSmsMinute));
                if (MessageCount > decimal.Zero)
                {
                    this.timer1.Enabled = false;
                    foreach (var item in list)
                    {
                        var model = new PushToSmsModels.Req_getmobileModel
                        {
                            id = item.userid,
                            key = Common.CreateKey(item.userid)
                        };
                        var json = JsonConvert.SerializeObject(model);
                        var mobile = PushToMessage.get_usermobile(json);
                        var status = decimal.One;
                        //时间超出转为短信发送
                        if (!string.IsNullOrEmpty(mobile) && mobile.Length == 11)
                        {
                            var Custom = new Random(DateTime.Now.Millisecond).Next(10, 98).ToString();
                            var content = item.messagetype == 0 ? string.Format("{0} ({1})", item.content, "请登平台查看") : item.content;
                            var entity = new PushToSmsModels.smssendModel
                            {
                                ID = Common.GetRandomID().ToString(),
                                SchoolCode = item.schoolcode,
                                Mobile = mobile,
                                Content = content,
                                InputDate = DateTime.Now,
                                Status = -1,
                                Description = string.Empty,
                                TaskId = string.Empty,
                                TaskStatus = decimal.MinusOne,
                                TaskType = 0,
                                Custom = Custom,
                                LevelNo = 0,
                                VerifyStatus = 1
                            };
                            var s = PushToMessage.set_smssend(entity);
                            status = s ? decimal.Zero : decimal.One;
                        }
                        else
                        {
                            status = 9;
                            Common.WriteToLogFile(String.Format("手机长度错误,手机号码：{0}）", mobile));
                        }
                        var result = PushToMessage.update_pushtosms(item.schoolcode, item.userid, item.messageid, status);
                        Common.WriteToLogFile(String.Format("提交消息状态（schoolcode:{0},userid{1},messageid:{2},status:{3},DB:{4}）", item.schoolcode, item.userid, item.messageid, status, result));
                    }
                    //重新开启定时器使用
                    this.timer1.Enabled = true;
                    timer1.Interval = 1000;//1秒钟执行一次
                }
                else
                {
                    timer1.Interval = IntervalTime; //每隔｛IntervalTiem｝毫秒 执行一次
                }
            }
            catch (Exception ex)
            {
                Common.WriteToLogFile(String.Format("消息监控扫描数据异常:{1}", ex.ToString()));
            }
        }


        /// <summary>
        /// 监听
        /// </summary>
        public void RecevieMessage()
        {
            try
            {
                while (IsStatus)
                {
                    if (MessageQueue.Exists(MSMQ.MSMQHelper.MsmqString))
                    {
                        MessageQueue mq = new MessageQueue(MSMQ.MSMQHelper.MsmqString);
                        System.Messaging.Message message = mq.Receive();
                        message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                        if (message != null)
                        {
                            //消息类型 对应 FSMP.MSMQ.SendMessage.MessageType值
                            var Lable = message.Label.ToString();
                            string Body = message.Body.ToString();
                            Common.WriteToLogFile(string.Format("Lable:{0},Body:{1}", Lable, Body));
                            int success = 0;
                            int fail = 0;
                            if (Lable == ((int)MSMQ.MessageType.Message).ToString())
                            {
                                var messagelist = MSMQ.MessageDeserialize.MessageList(Body);
                                success = 0;
                                fail = 0;
                                foreach (var item in messagelist)
                                {
                                    var s = PushToMessage.set_pushtosms(item.SchoolCode, item.UserId, item.MessageId, (int)MessageType.Message, item.Content, decimal.MinusOne);
                                    if (s)
                                    {
                                        success++;
                                    }
                                    else
                                    {
                                        fail++;
                                    }
                                }
                                Common.WriteToLogFile(string.Format("消息执行完毕，成功：{0},失败:{1}", success, fail));
                            }
                            else if (Lable == ((int)MSMQ.MessageType.MessageRead).ToString())
                            {
                                var messageread = MSMQ.MessageDeserialize.MessageStatus(Body);
                                if (messageread != null)
                                {
                                    var s = PushToMessage.update_pushtosms(messageread.SchoolCode, messageread.UserId, messageread.MessageId, decimal.One);
                                    Common.WriteToLogFile(string.Format("消息执行完毕，执行结果：{0}", s));
                                }
                            }
                            else if (Lable == ((int)MSMQ.MessageType.WorkBook).ToString())
                            {
                                var workbookllist = MSMQ.MessageDeserialize.WorkBoolList(Body);
                                success = 0;
                                fail = 0;
                                foreach (var item in workbookllist)
                                {
                                    var s = PushToMessage.set_pushtosms(item.SchoolCode, item.UserId, item.WorkBookId, (int)MessageType.WorkBook, item.Content, decimal.MinusOne);
                                    if (s)
                                    {
                                        success++;
                                    }
                                    else
                                    {
                                        fail++;
                                    }
                                }
                                Common.WriteToLogFile(string.Format("消息执行完毕，成功：{0},失败:{1}", success, fail));

                            }
                            else if (Lable == ((int)MSMQ.MessageType.WorkBookRead).ToString())
                            {
                                var wookbookread = MSMQ.MessageDeserialize.WorkBookStatus(Body);
                                success = 0;
                                fail = 0;
                                if (wookbookread != null)
                                {
                                    var s = PushToMessage.update_pushtosms(wookbookread.SchoolCode, wookbookread.UserId, wookbookread.WorkBookId, decimal.One);
                                    Common.WriteToLogFile(string.Format("消息执行完毕，执行结果：{0}", s));
                                }
                            }
                            else
                            {
                                Common.WriteToLogFile(String.Format("未处理的MQ记录(Lable:{0},Body:{1})", Lable, Body));
                            }
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                    else
                    {
                        Common.WriteToLogFile(String.Format("{0}不存在", MSMQ.MSMQHelper.MsmqString));
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteToLogFile(String.Format("接收MQ消息异常错误：{0}", ex.ToString()));
            }
        }

    }
}


    }
}
