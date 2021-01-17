namespace ConsoleTypesetting
{
    public static class SampleExtension
    {
        public static int WordCount(this string message)
        {
            return message.Split(new char[] { ' ', ',', '?' }, System.StringSplitOptions.RemoveEmptyEntries).Length;
        }

    }
}