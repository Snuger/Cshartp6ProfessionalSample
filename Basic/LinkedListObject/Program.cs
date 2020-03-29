using System;

namespace LinkedListObject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            LinkedList list = new LinkedList();
            list.LinkedAdd("23232");
            list.LinkedAdd(23232);
            list.LinkedAdd("wq vb r ");


            foreach (object iobj in list) {
                Console.WriteLine(iobj);
            }

            Console.ReadLine();
        }
    }
}
