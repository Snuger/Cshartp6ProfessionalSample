using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JmeterIntegrated.Utilities
{
    public class ProcessStartOpearHelper
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="arguments">参数</param>
        /// <returns></returns>
        public static async Task<bool> ProcessRun(string command, string arguments)
        {
            return await Task<bool>.Run(() =>
            {
                try
                {
                    ProcessStartInfo process = new ProcessStartInfo(command, arguments) { RedirectStandardOutput = false };
                    Process proc = Process.Start(process);
                    proc.WaitForExit();
                }
                catch (System.Exception)
                {
                    return false;
                }
                return true;
            });

        }

    }
}