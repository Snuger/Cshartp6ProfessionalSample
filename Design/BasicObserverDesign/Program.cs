using System;

namespace BasicObserverDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectSubject subject = new ConnectSubject();
            subject.Add(new ConnectObserver("张三", subject));
            subject.Add(new ConnectObserver("李四",subject));
            subject.SubjectState = "三天打鱼";
            subject.Nofify();
            Console.ReadLine();
        }
    }
}
