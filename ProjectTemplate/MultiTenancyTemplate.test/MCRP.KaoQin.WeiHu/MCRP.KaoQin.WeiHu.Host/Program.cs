using MCRP.MultiTenancy.Host;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace MCRP.KaoQin.WeiHu.Host
{
    class Program
    {
        static void Main(string[] args)
        {
             
            DefaultWebHostBuilder.CreateWebHostBuilder(args)
                .Build().Run();
        } 
        
    }
}
