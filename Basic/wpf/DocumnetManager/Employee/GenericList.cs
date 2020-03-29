using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager
{
    public class GenericList<TEmployee>
        where TEmployee:Employee
    {

        /// <summary>
        /// 内部类
        /// </summary>
        public class Node {
            public Node(TEmployee employee)
            {
                Next = null;
                Data = employee;
            }

            public Node(Node next, TEmployee data)
            {
                Next = next;
                Data = data;
            }

            public Node Next { get; set; }
            public TEmployee Data { get; set; }
        }



        private Node head;

        public void AddNode(TEmployee employee) {
            Node n = new Node(employee) { Next = head};
            head = n;
        }

        public IEnumerator<TEmployee> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public TEmployee GetEmployee(string s) {
            Node current = head;
            TEmployee employee = default(TEmployee);
            while (current != null) {
                if (current.Data.Name == s)
                {
                    employee = current.Data;
                    break;
                }
                else {
                    current = current.Next;
                }
            }
            return employee;
        }     

    }
}
