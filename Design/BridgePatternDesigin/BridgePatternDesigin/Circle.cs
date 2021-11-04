using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePatternDesigin
{
    internal class Circle:Shape
    {
        protected int x, y, radius;

        public Circle(int x, int y, int radius, IDrawAPI drawAPI):base(drawAPI)
        {           
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public override string Draw() => _drawAPI.DrawCircle(this.x, this.y, this.radius);
    }
}
