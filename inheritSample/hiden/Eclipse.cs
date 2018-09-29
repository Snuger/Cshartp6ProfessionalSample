using System;
using System.Collections.Generic;
using System.Text;

namespace inheritSample.hiden
{
    public class Eclipse : Sharp
    {
        public new void Draw()
        {
            base.Draw();
        }
    }
}
