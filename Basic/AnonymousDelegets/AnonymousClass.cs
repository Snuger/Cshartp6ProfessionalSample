using System;
using System.Collections.Generic;
using System.Text;

namespace AnonymousDelegets
{
    public class AnonymousClass
    {
        private int someVal;

        public AnonymousClass(int someVal)
        {
            this.someVal = someVal;
        }

        public int AnonymousMethod(int x) => x + someVal;

    }
}
