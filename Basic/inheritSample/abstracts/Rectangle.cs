using System;
using System.Collections.Generic;
using System.Text;

namespace inheritSample.abstracts
{
    public abstract class Rectangle : Sharpe
    {
        protected Rectangle()
        {
        }

        public override void Moves(int x, int y)
        {
            Posintion = new Posintion() {
                 X=x,
                 Y=y
            };

            //base.Moves(width, height);
        }

        public override void ReSize(Size size)
        {
            Size = size;
            //throw new NotImplementedException();
        }

     

    }
}
