using System;
using System.Collections.Generic;
using System.Text;

namespace Variance
{
    interface IDisplay<in T>
    {
        void Show(T item);
    }
}
