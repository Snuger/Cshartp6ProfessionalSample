using MCRP.ServiceHost.K8S;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace Template.Host
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await DefaultWebHostBuilder.CreateWebHostBuilder(args)
                 .Build().RunAsync();
        }

    }
}
