using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InterlockedExchange_Example
{
    public class ExampleObject
    {
         int unsafeInstanceCount = 0;//不使用原子操作
         int safeInstanceCount = 0;//使用原子操作

        public int SafeInstanceCount {
            get => Interlocked.Increment(ref safeInstanceCount);
            set => safeInstanceCount = value;
        }

        public int UnsafeInstanceCount {
            get => unsafeInstanceCount;
            set => unsafeInstanceCount = value;
        }
    }
}
