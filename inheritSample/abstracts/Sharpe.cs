using System;
using System.Collections.Generic;
using System.Text;

namespace inheritSample.abstracts
{

    public class Posintion {
        public int X { get; set; }

        public int Y { get; set; }

    }


    public class Size
    {
        public int Width { get; set; }

        public int Height { get; set; }

    }

    public abstract class Sharpe
    { 
        /// <summary>
        /// 
        /// </summary>
        public  Posintion Posintion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size Size { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y) {
            Console.WriteLine($"设置位置");
            Posintion = new Posintion { X = x, Y = y };
        }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="width"></param>
          /// <param name="height"></param>
        public virtual void Moves(int width, int height) {
            Size = new Size() { Width = width, Height = height };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public abstract void ReSize(Size size);


  

    }
}
