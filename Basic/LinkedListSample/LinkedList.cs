using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedListSample
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public LinkedListNode<T> First { get; private set; }

        public LinkedListNode<T> Last { get; private set; }


        public LinkedListNode<T> add(T obj)
        {
            var node = new LinkedListNode<T>(obj);

            if (First == null)
            {
                First = node;
                Last = First;
            }
            else
            {
                Last.Next = node;
                Last = node;
            }
            return node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()=> GetEnumerator();
    }
}
