using inheritSample.Inheritance;
using System;


namespace inheritSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle(new Size() { Width = 200, Height = 100 }, new Position() { X = 99, Y = 99 });

            rectangle.Draw();


            rectangle.Moves(new Position() { X = 20, Y = 20 });

            rectangle.Draw();



            Shape shape = new Shape(new Size() { Width = 10, Height = 10 },new Position() {X=0,Y=0});


            shape.Draw();

            Console.ReadLine();




        }
    }
}
