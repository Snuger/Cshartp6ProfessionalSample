namespace QuestionLibrary
{
    public class NoRepeatingCharsSolution
    {
        public int LengthOfLongestSubstring(string s)
        {
            int existsLength = 0;
            int repCount=0;
            for (int i = 0; i < s.Length; i++)
            {
                for (int t = 1; t < s.Length-i; t++)
                {
                    string sub = s.Substring(i, t);
                    string newStr=s.Remove(i,t);
                    if(newStr.Contains(sub)&&(t-i)>existsLength) existsLength = t-i;
                }

            }
            return existsLength;
        }

    }
}