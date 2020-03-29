using System;
using System.Collections.Generic;
using System.Text;

namespace Variance
{
    public class ShapeDisplay : IDisplay<Shape>
    {
        public void Show(Shape item) =>Console.WriteLine($"{item.GetType().Name} Width:{item.Width} height:{item.Height}");
    }
}
