using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
    class Program
    {

        private static CancellationTokenSource cancellationToken;

        static void Main(string[] args)
        {
            //Console.WriteLine("测试开始...");
            //Console.ReadLine();
            //ImgAsyncDownload imgAsyncDownload = new ImgAsyncDownload();
            //imgAsyncDownload.downloadImg();
            //Console.ReadLine();

            // Dohandel();
            // Console.ReadLine();


            Console.WriteLine("请输入操作项目:");
            string input = string.Empty;

            while (true) {
                input = Console.ReadLine();
                switch (input) {
                    case "exit":
                        DoWorkStop();
                        break;
                    case "start":
                         //DoworkTaskStart();
                        CallDoWorkAsync();
                        break;
                    default:
                        //DoworkTaskStart();
                        if (!string.IsNullOrWhiteSpace(input)) {
                            Console.WriteLine($">{input} commond not found");                           
                        }                       
                        break;
                }

            }

        }




        static async void Dohandel() {

            try
            {
                // ThrowFirstExecption(2000, "你妹啊，错误了啊");
                Task t1 = ThrowFirstExecption(2000, "你妹啊，第一次..");
                Task t2 = ThrowFirstExecption(1000, "你妹啊，第二次...");
                await Task.WhenAll(t1, t2);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.InnerException);
            }


        }

        //人为抛出异常
        private static async Task ThrowFirstExecption(int ms, string message)
        {
            await Task.Delay(ms);
            throw new Exception(message);

        }


        private static void DoWorkStop(){
            cancellationToken?.Cancel();
        }


        private async static void DoworkTaskStart() {
            cancellationToken = new CancellationTokenSource();
            try
            {
                await DoWorkAsync(3000000, cancellationToken.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);               
            }         
        }

        private static void CallDoWorkAsync() {
            try
            {
                cancellationToken = new CancellationTokenSource();
                Task task = DoWorkAsync(6000, cancellationToken.Token);
                task.ContinueWith(t =>
                {
                   // Console.WriteLine($"Task status：{t.Status}");

                    if (t.IsCompleted)
                    {
                        Console.WriteLine();

                        if (cancellationToken.IsCancellationRequested)
                        {
                            Console.WriteLine("job has Canceled...");
                        }
                        else {
                            Console.WriteLine("job has Completed...");
                        }                         
                    }                 
                       
                    if (t.IsFaulted)
                        Console.WriteLine("job has Faulted...");
                    if (t.IsCanceled)
                        Console.WriteLine("job has Canceled...");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);              
            }         

        }


        private static async Task DoWorkAsync(int ms, CancellationToken token)
        {            
            bool stop = false;
            int span = 300;
            int wait = span;
            while (!stop) {
                await Task.Delay(span);               
                Console.Write("->");
                wait += span;               

                if (wait >= ms)
                    stop = true;

                if (token.IsCancellationRequested)
                    stop = true;
            }          
        }
    }
}
