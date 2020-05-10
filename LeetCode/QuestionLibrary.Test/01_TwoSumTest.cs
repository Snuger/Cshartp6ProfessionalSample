using System;
using Xunit;
using QuestionLibrary;


namespace QuestionLibrary.Test
{
    public class TwoSumTest
    {
        private TwoSumSolution _twoSum;
        public TwoSumTest()
        {
            _twoSum = new TwoSumSolution();
        }

        [Fact]
        public void TestTwoSum()
        {
            var result = _twoSum.TwoSum(new int[] { 2, 7, 11, 15 }, 9);
            Assert.Equal(new int[] { 0, 1 }, result);
            Assert.Throws<Exception>(() => _twoSum.TwoSum(new int[] { 3, 3 }, 7));


        }
    }
}
