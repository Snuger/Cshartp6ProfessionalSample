using System;
using System.Data;
using Dapper;
using Npgsql;


namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("查询151数据库");
                using (IDbConnection connection = new NpgsqlConnection("Uid=mcrp;Pwd=mcrp;Server=172.19.30.151;Port=5432;Database=mcrpdb;"))
                {
                    Console.WriteLine("151数据库连接创建成功");
                    var outPut = connection.QuerySingle<string>("select  id from  mcrp_dev.dev_kaifarw limit 1 offset 0");
                    Console.WriteLine($"151数据库查询结果{outPut}");
                }

                Console.WriteLine("查询152数据库");
                 
                using (IDbConnection con = new NpgsqlConnection("Uid=medi_oa;Pwd=medi_oa@123;Server=172.19.32.152;Port=5432;Database=medi_oa;Pooling=false; Timeout = 300;CommandTimeout = 300"))
                {
                    Console.WriteLine("152数据库连接创建成功");                
                
                    var outPut = con.QuerySingle<string>("select id from cmp_wim_01.wim_shiwugs_0001 limit 1 offset 0");
                    Console.WriteLine($"231数据库查询结果{outPut}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }           
            Console.Read();
        }
    }
}
