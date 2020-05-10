using System;

namespace QuestionLibrary
{
    public class TwoSumSolution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int t = i + 1; t < nums.Length; t++)
                {
                    if (nums[i] + nums[t] == target)
                        return new int[] { i, t };
                }
            }
            throw new Exception("没有找到合适的结果");
        }

    }
}
