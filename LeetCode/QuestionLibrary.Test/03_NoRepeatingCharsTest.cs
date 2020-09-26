using Xunit;

namespace QuestionLibrary.Test
{
    public class NoRepeatingCharsTest
    {
        protected readonly NoRepeatingCharsSolution _solution;
        public NoRepeatingCharsTest( )
        {
            _solution = new NoRepeatingCharsSolution();
        }

        [Fact]
        public void TestNoRepeatingChars()
        {
            Assert.Equal(3,_solution.LengthOfLongestSubstring("abcabcbb"));
            
        }
        
    }
}