using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListSample
{
    public class LinkedListNode<T>
    {
        public LinkedListNode(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public LinkedListNode<T> Next { get; internal set; }

        public LinkedListNode<T> Prev { get; internal set; }

    }
}
