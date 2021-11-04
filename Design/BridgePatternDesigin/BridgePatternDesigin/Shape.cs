using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePatternDesigin
{
    internal abstract class Shape
    {
        protected IDrawAPI _drawAPI;

        public Shape(IDrawAPI drawAPI)
        {
            _drawAPI=drawAPI;
        }

        public abstract string Draw();
    }
}
