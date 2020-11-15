using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceInjectionSample.Service
{
    class Services3 : IService3, IDisposable
    {

        private bool _disposed;

        public string MyKey { get; }

        public Services3(string myKey)
        {
            MyKey = myKey;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Console.WriteLine("Service3.Dispose");
            _disposed = true;
        }

        public void Write(string message)
        {
            Console.WriteLine($"Service3: {message}, MyKey = {MyKey}");
        }
    }
}
