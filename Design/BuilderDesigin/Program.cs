using System;

namespace BuilderDesigin
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultDbContext context = new DefaultDbContext(new DbContextOptions("AppName"));
   
            Console.ReadLine();
        }
    }
}
