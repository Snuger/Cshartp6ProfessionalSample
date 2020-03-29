using System;
using System.Collections.Generic;
using System.Text;

namespace Variance
{
     public class MyClass<T>
    {
        private static int count = 0;

        public MyClass()
        {
            count++;
        }

        public  int Count { get => count;  }
    }


    public class MyClass : MyClass<string>
    {
        public MyClass()
        {
        }
    }
}
