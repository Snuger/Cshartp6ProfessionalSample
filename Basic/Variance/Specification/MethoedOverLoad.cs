using System;
using System.Collections.Generic;
using System.Text;

namespace Variance.Specification
{
    public  class MethoedOverLoad
    {
        public void Foo<T>(T Obj) {

            Console.WriteLine($"Foo<T>(Obj)  type:{Obj.GetType().Name}");
        }

        public void Foo(int x) {
            Console.WriteLine($"Foo(int x)");
        }

        public void Foo<T1, T2>(T1 obj1, T2 obj2) {

            Console.WriteLine($"Foo<T1, T2>(T1 obj1, T2 obj2); type:{obj1.GetType().Name} {obj2.GetType().Name}");
        }

        public void Foo<T>(int obj1, T obj2) {
            Console.WriteLine($"Foo<T1, T2>(int obj1, T2 obj2); type:{obj2.GetType().Name}");

        }

        public void Bar<T>(T obj) {

            Foo(obj);
        }




    }
}
