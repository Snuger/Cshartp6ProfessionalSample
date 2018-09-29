using System;
using System.Collections.Generic;
using System.Text;

namespace inheritSample.Inheritance
{
    public class Position {

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() => $"X:{X},Y:{Y}";
 
    }

    public class Size {

        public int Width { get; set; }
        public int Height { get; set; }

        public override string ToString()=>$"Width:{Width},Height:{Height}";

    }



    public class Shape
    {


        public Shape(int x, int y, int hieght, int width) {

            Position = new Position() { X = x, Y = y };

            Size = new Size() { Width = width, Height = hieght };

        }

        public Shape(Size size, Position position)
        {
            Size = size;
            Position = position;
        }

        public Size Size { get; set; }

        public Position Position { get; set; }


        public virtual void Draw() => Console.WriteLine($"Sharp Position:{Position},Size:{Size}");



        public virtual void Moves(Position position) {
            Position = new Position()
            {
                X = position.X,
                Y = position.Y
            };
            Console.WriteLine($"Move to {Position}");
        }

    }
}
