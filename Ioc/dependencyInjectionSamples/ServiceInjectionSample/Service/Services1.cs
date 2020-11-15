using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceInjectionSample.Service
{
    public class Services2 : IDisposable
    {
        private bool _isDispose;


        public void Write(string message) {
            Console.WriteLine($"{nameof(Services2)}:{message}");
        }

        public void Dispose()
        {
            if (_isDispose)
                return;
            Console.WriteLine($"{nameof(Services2)}.Dispose");
            _isDispose = true;
        }
    }
}
