using System;

namespace BridgePatternDesigin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shape redShape = new Circle(100, 199, 10, new RedCircle());
            Shape greenShape = new Circle(100,182,2,new GreenCircle());
            Console.WriteLine(redShape.Draw());
            Console.WriteLine(greenShape.Draw());
            Console.ReadLine();
        }
    }
}
