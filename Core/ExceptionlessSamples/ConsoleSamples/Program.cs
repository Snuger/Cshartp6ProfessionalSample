using System;
using Exceptionless;

namespace ConsoleSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                 throw new ApplicationException(Guid.NewGuid().ToString());
            }
            catch (System.Exception ex)
            {
                 ex.ToExceptionless().Submit();               
            }
        }
    }
}
