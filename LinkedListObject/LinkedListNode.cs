using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListObject
{
    /// <summary>
    /// 所谓链表，就是结构内会记录一个节点是谁
    /// </summary>
    public class LinkedListNode
    {
        public LinkedListNode(object value)
        {
            Value = value;
        }

        public object Value { get;  }


        public LinkedListNode Next { get; internal set; }
        public LinkedListNode Prev { get; internal set; }
   
    }
}
