using System;
using System.Diagnostics.Tracing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventSourceSamples
{
    class Program
    {

       // private static EventSource SampleEventSource = new EventSource("wox-EventSourceSamples");

        static void Main(string[] args)
        {           

            Console.WriteLine(SampleEventSource.log.Guid);
            Console.WriteLine(SampleEventSource.log.Name);

            Console.ReadLine();

            NetworkRequestSample().Wait();

            Console.ReadLine();
            SampleEventSource.log.Dispose();
        }



        protected static async Task NetworkRequestSample() {

            try
            {
                string url = "http://www.cninnovation.com";
                using (var client = new HttpClient())
                {
                    SampleEventSource.log.Write("NetWork",new {Info= $"calling {url}" });

                   string result=await client.GetStringAsync(url);

                    SampleEventSource.log.Write("NetWork", new { Info = $"completed call to {url}, result string length: {result.Length}" });

                }
            }
            catch (Exception ex)
            {
                SampleEventSource.log.Write("Network Error", new EventSourceOptions { Level = EventLevel.Error }, new { Message = ex.Message, Result = ex.HResult });
                Console.WriteLine(ex.Message);
            }           
        }

        private static void ParallelRequestSample() {
            SampleEventSource.log.RequestBegin();
            Parallel.For(0, 20, x =>
            {
                ProcessTaskSync(x).Wait();
            });
            
        }

        protected static async Task ProcessTaskSync(int x) {
            SampleEventSource.log.ProcessStart(x);

            var r = new Random();
            await Task.Delay(r.Next(500));

            using (HttpClient client = new HttpClient()) {
                var response = await client.GetAsync("http://www.bing.com");
            }

            SampleEventSource.log.ProcessStop(x);

        }

    }
}
