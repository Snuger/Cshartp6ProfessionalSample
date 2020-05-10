using System;
using Xunit;
using QuestionLibrary.Model;
using Xunit.Abstractions;

namespace QuestionLibrary.Test
{
    public class TwoNumAddSolutionTest
    {

        private readonly ITestOutputHelper _outputHelper;
        private TwoNumAddSolution _solution;
        public TwoNumAddSolutionTest(ITestOutputHelper outputHelper)
        {
            _solution = new TwoNumAddSolution();
            _outputHelper = outputHelper;
        }

        protected ListNode initData(int[] arg)
        {
            ListNode dummy = new ListNode(-1);
            ListNode preNode = dummy;
            for (int t = 0; t < arg.Length; t++)
            {
                ListNode currentNode = new ListNode(arg[t]);
                preNode.next = currentNode;
                preNode = preNode.next;
            }
            return dummy.next;
        }

        [Fact]
        public void TastAddTwoNumberByRecursive()
        {

            Assert.Equal(initData(new int[] { 0 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 0 }), initData(new int[] { 0 })));
            Assert.Equal(initData(new int[] { 8, 1 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 9 }), initData(new int[] { 9 })));
            Assert.Equal(initData(new int[] { 0, 1 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 5 }), initData(new int[] { 5 })));
            Assert.Equal(initData(new int[] { 1, 8 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 1, 8 }), initData(new int[] { 0 })));
            Assert.Equal(initData(new int[] { 0, 1, 0 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 1 }), initData(new int[] { 9, 9 })));
            Assert.Equal(initData(new int[] { 7, 3 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 0 }), initData(new int[] { 7, 3 })));
            Assert.Equal(initData(new int[] { 9, 2, 4, 1, 7, 4, 0, 9, 9 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 1, 6, 0, 3, 3, 6, 7, 2, 0, 1 }), initData(new int[] { 6, 3, 0, 8, 9, 6, 6, 9, 6, 1 })));
            Assert.Equal(initData(new int[] { 7, 0, 8 }), _solution.AddTwoNumberByRecursive(initData(new int[] { 2, 4, 3 }), initData(new int[] { 5, 6, 4 })));

        }

        [Fact]
        public void TestAddTwoNumber()
        {
            Assert.Equal(initData(new int[] { 0 }), _solution.AddTwoNumber(initData(new int[] { 0 }), initData(new int[] { 0 })));
            Assert.Equal(initData(new int[] { 8, 1 }), _solution.AddTwoNumber(initData(new int[] { 9 }), initData(new int[] { 9 })));
            Assert.Equal(initData(new int[] { 0, 1 }), _solution.AddTwoNumber(initData(new int[] { 5 }), initData(new int[] { 5 })));
            Assert.Equal(initData(new int[] { 1, 8 }), _solution.AddTwoNumber(initData(new int[] { 1, 8 }), initData(new int[] { 0 })));
            Assert.Equal(initData(new int[] { 0, 1, 0 }), _solution.AddTwoNumber(initData(new int[] { 1 }), initData(new int[] { 9, 9 })));
            Assert.Equal(initData(new int[] { 7, 3 }), _solution.AddTwoNumber(initData(new int[] { 0 }), initData(new int[] { 7, 3 })));
            Assert.Equal(initData(new int[] { 9, 2, 4, 1, 7, 4, 0, 9, 9 }), _solution.AddTwoNumber(initData(new int[] { 1, 6, 0, 3, 3, 6, 7, 2, 0, 1 }), initData(new int[] { 6, 3, 0, 8, 9, 6, 6, 9, 6, 1 })));
            Assert.Equal(initData(new int[] { 7, 0, 8 }), _solution.AddTwoNumber(initData(new int[] { 2, 4, 3 }), initData(new int[] { 5, 6, 4 })));

        }
    }
}