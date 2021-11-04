using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePatternDesigin
{
    internal class RedCircle : IDrawAPI
    {
        public string DrawCircle(int x,int y,int radius) =>$"draw  {nameof(RedCircle)},x:{x},y:{y},radius:{radius};";
    }
}
