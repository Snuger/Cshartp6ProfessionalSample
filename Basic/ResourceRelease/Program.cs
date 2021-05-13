using System;
using System.Runtime.InteropServices;

namespace ResourceRelease
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // using (MyClassSample sample = new MyClassSample("张三"))
            // {
            //     //sample.Dispose();
            // }
            // MyClassSample sample = new MyClassSample("张三");
            // GC.Collect();
            // //sample.Dispose();
            // if (sample == null)
            //     Console.WriteLine("对偶已被释放");


            using (MyClassSample sample1 = new MyClassSample("张三"))
                if (sample1 == null)
                    Console.WriteLine("对偶已被释放");
        }
    }

    public class MyClass : IDisposable
    {
        private IntPtr NativeResource { get; set; } = Marshal.AllocHGlobal(100);
        public Random ManagedResource { get; set; } = new Random();

        private bool disposed;

        ~MyClass()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            if (!disposed)
            {
                if (NativeResource != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(NativeResource);
                    NativeResource = IntPtr.Zero;
                }
                disposed = true;
            }
        }

        protected virtual void Dispose(bool disposeing)
        {
            if (disposed)
                return;
            if (disposeing)
            {
                if (NativeResource != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(NativeResource);
                    NativeResource = IntPtr.Zero;
                }
            }
            disposeing = true;
        }
    }

    public class MyClassSample : IDisposable
    {
        public string Name { get; set; }

        public MyClassSample(string name)
        {
            Name = name;
        }

        public MyClassChild child { get; set; } = new MyClassChild();

        private bool disposed;

        ~MyClassSample()
        {
            Console.WriteLine($"MyClassSample { Name} 被释放");
            disposed = false;
        }

        public void Dispose()
        {
            if (!disposed)
                child.Dispose();
            disposed = true;
        }
    }

    public class MyClassChild : IDisposable
    {
        private bool disposed;

        ~MyClassChild()
        {
            disposed = false;

        }

        public void Dispose()
        {
            if (!disposed)
                Console.WriteLine("MyClassChild被释放");
            disposed = true;
        }
    }



}
