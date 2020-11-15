using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceInjectionSample.Service
{
    public class Services1 : IDisposable
    {
        private bool _isDispose;


        public void Write(string message) {
            Console.WriteLine($"{nameof(Services1)}:{message}");
        }

        public void Dispose()
        {
            if (_isDispose)
                return;
            Console.WriteLine($"{nameof(Services1)}.Dispose");
            _isDispose = true;
        }
    }
}
