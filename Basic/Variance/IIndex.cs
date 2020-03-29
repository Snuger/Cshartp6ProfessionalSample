using System;
using System.Collections.Generic;
using System.Text;

namespace Variance
{
    interface IIndex<out T>
    {
        T this[int index] { get; }


        int Count { get; }
    }
}
