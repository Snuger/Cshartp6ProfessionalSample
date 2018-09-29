using System;
using System.Collections.Generic;
using System.Text;

namespace Variance
{
    public class RectangleCollection : IIndex<Rectangle>
    {

        private Rectangle[] rectangle = new Rectangle[3] {
            new Rectangle(){ Width=100,Height=20 },
            new Rectangle(){ Width=150,Height=65 },
            new Rectangle() { Width = 21,Height = 66 }
        };

        private static RectangleCollection collection;

        int IIndex<Rectangle>.Count => rectangle.Length;

        public static RectangleCollection GetRectangleCollection() => collection ?? (collection = new RectangleCollection());

        public Rectangle this[int index]
        {
            get{
                if (index < 0 || index > rectangle.Length) {
                    throw new ArgumentOutOfRangeException("index");
                }
                return rectangle[index];
            }
        }       

    }
}
