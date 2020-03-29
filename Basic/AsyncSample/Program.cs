using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncSample
{
    class Program
    {
        static void Main(string[] args)
        {
            CallerWithAsync();
            /*
           
            Console.WriteLine("此时能不能输入...");
            Console.ReadLine();

            var t1 = GetStringAsync("test ContinueWith...");
            t1.ContinueWith(t=> {
               string s= t.Result;
                WriteLine(s);
            });


            string result=GetStringAsync("CallerWithAwaiter").GetAwaiter().GetResult();

            Console.WriteLine(result);


            MuitipleAsync();

            Console.WriteLine("等待多事务处理...");
            Console.ReadLine();

            


            MultipleAsyncMethodsWithCombinators2();

            Console.WriteLine("等待多事务处理...");
            Console.ReadLine();*/


            //            TastAsyncPattm();
            //downloadImg();
            Console.WriteLine("测试开始...");
            Console.ReadLine();
            ImgAsyncDownload imgAsyncDownload = new ImgAsyncDownload();
            imgAsyncDownload.downloadImg();
            Console.ReadLine();

        }
     

        static string GetString(string name)
        {
            Task.Delay(3000).Wait();
            return $"hello {name}";
        }

        private static async void CallerWithAsync()
        {
            string result = await GetStringAsync("李四");          
            Console.WriteLine(result);
           
        }      


        static Task<string> GetStringAsync(string name) {
            return Task.Run<string>(()=>{
                return GetString(name);
            });

        }

        private static async void MuitipleAsync() {

            string s1 = await GetStringAsync("你妹的，");
            string s2 = await GetStringAsync("现在才来");

            WriteLine($"Finished both methods.\n Result 1: {s1}\n Result 2: {s2}");

        }
        private static async void MultipleAsyncMethodsWithCombinators2() {
            Task<string> s1 =  GetStringAsync("你妹的，");
            Task<string> s2 =  GetStringAsync("现在才来");
            string[] result = await Task.WhenAll(s1,s2);
            WriteLine($"Finished both methods.\n Result 1: {result[0]}\n Result 2: {result[1]}");

        }


        private static async void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> s1 = GetStringAsync("你妹的，");
            Task<string> s2 = GetStringAsync("现在才来");      
            
            await Task.WhenAll(s1, s2);
            WriteLine($"Finished both methods.\n Result 1: {s1.Result}\n Result 2: {s2.Result}");

        }


    }
}
