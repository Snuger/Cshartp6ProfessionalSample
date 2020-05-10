using System;

namespace QuestionLibrary.Model
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

        public override string ToString()
        {
            string str = $"{val}";
            ListNode node = next;
            while (node != null)
            {
                str += $"->{node.val}";
                node = node.next ?? null;
            }
            return str;
        }
    }
}