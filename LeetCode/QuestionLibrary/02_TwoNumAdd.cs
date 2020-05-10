using System;
using QuestionLibrary.Model;

namespace QuestionLibrary
{
    public class TwoNumAddSolution
    {

        /// <summary>
        /// ????
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode AddTwoNumberByRecursive(ListNode l1, ListNode l2)
        {
            return dealNode(l1, l2, false);
        }

        public ListNode AddTwoNumber(ListNode l1, ListNode l2)
        {
            ListNode node = new ListNode(-1);
            ListNode firstNode = node;
            bool intoOne = false;
            ListNode node1 = l1; ListNode node2 = l2;
            while (node1 != null || node2 != null)
            {
                int subResult = intoOne ? (node1.val + node2.val + 1) : (node1.val + node2.val);
                intoOne = subResult >= 10 ? true : false;
                ListNode currentNode = new ListNode((subResult % 10));
                firstNode.next = currentNode;
                firstNode = firstNode.next;
                if (node1.next == null && node2.next == null && !intoOne)
                    break;
                node1 = node1.next ?? new ListNode(0);
                node2 = node2.next ?? new ListNode(0);
            }
            return node.next;
        }



        /// <summary>
        /// ????
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="intoOne"></param>
        /// <returns></returns>
        public ListNode dealNode(ListNode l1, ListNode l2, bool intoOne)
        {
            int nodeVal = 0;
            if (l1.val == 0 && l1.next == null && l2.val == 0 && l2.next == null && !intoOne)
                return new ListNode(0);

            if (l1.val == 0 && l2.val == 0 && intoOne)
            {
                ListNode node = new ListNode(1);
                if (l1.next != null || l2.next != null)
                    node.next = dealNode(l1.next ?? new ListNode(0), l2.next ?? new ListNode(0), false);
                return node;
            }
            else
            {
                int compare = l1.val + l2.val;
                bool _inotOne = false;
                if (intoOne)
                    compare = compare + 1;
                if (compare >= 10)
                {
                    _inotOne = true;
                    nodeVal = (compare % 10);
                }
                else
                {
                    nodeVal = compare;
                }
                ListNode outNode = new ListNode(nodeVal);
                if (l1.next == null && l2.next == null && !_inotOne)
                    return outNode;
                outNode.next = dealNode((l1.next) ?? new ListNode(0), l2.next ?? new ListNode(0), _inotOne);
                return outNode;
            }

        }
    }
}