using System;
using System.Collections.Generic;
using System.Text;

namespace inheritSample.Inheritance
{
    public class Rectangle : Shape
    {
        public Rectangle(Size size, Position position) : base(size, position)
        {
        }

        public Rectangle(int x, int y, int hieght, int width) : base(x, y, hieght, width)
        {
        }

        public override void Draw()=>Console.WriteLine($"Rectangle with {Position} and {Size}");

        public override void Moves(Position position)
        {
            Console.WriteLine("Rectangle");
            base.Moves(position);
        }
    }

    public class Eclipse : Shape
    {
        public Eclipse(Size size, Position position) : base(size, position)
        {
        }

        public Eclipse(int x, int y, int hieght, int width) : base(x, y, hieght, width)
        {
        }
    }
}
