using System;

namespace LinkedListSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.add("hello workd");
            linkedList.add("java");

 
            foreach (var s in linkedList) {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }
    }
}
