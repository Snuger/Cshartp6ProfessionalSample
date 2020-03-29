using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedListObject
{
    public  class LinkedList: IEnumerable
    {       

       public LinkedListNode First { get; private set; }

        public LinkedListNode Last { get; private set; }


        public LinkedListNode LinkedAdd(object obj) {
            LinkedListNode node = new LinkedListNode(obj);
            if (First == null)
            {
                First = node;
                Last = First;
            }
            else {
                Last.Next = node;
                Last = node;
            }
            return node;          
        }


        public IEnumerator GetEnumerator()
        {
            LinkedListNode currentNode = First;
            while (currentNode!= null) {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }           
        }
    }
}
