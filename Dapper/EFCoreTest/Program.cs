using System;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest
{



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("查询151数据库");
            Default151DbContext context151 = new Default151DbContext();
            var objt151 = context151.KaiFaRwModels.FromSqlRaw("select  id from  mcrp_dev.dev_kaifarw limit 1 offset 0");
            Console.WriteLine(objt151?.FirstOrDefaultAsync().Id);

            Console.WriteLine("查询152 GP数据库");
            DefaultDbContext context = new DefaultDbContext();
             var objt=context.shiWuGSModels.FromSqlRaw("select id from cmp_wim_01.wim_shiwugs_0001 limit 1 offset 0");
            Console.WriteLine(objt?.FirstOrDefaultAsync().Id);  

            Console.Read();
        }
    }
}
