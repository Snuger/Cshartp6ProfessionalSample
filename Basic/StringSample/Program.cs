using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string atr = "helloword";
            string att = "hello";           

            Console.WriteLine(atr[2]);

            Console.WriteLine(string.Compare(atr, att));

            string[] arg = {"1223223","admin" };

            List<string> str = new List<string>();
            str.Add("a");
            str.Add("b");
            str.Add("c");
            str.Add("d");


            Console.WriteLine(string.Concat(arg));


            Console.ReadLine();

            Console.WriteLine("测试测试");



          　Console.WriteLine(string.Concat(str as IEnumerable<string>));

            //Console.WriteLine("Hello World!");
            Console.ReadLine();

            List<sbody> sbodies = new List<sbody>();
            sbodies.Add(new sbody(System.Guid.NewGuid().ToString(), "d"));
            sbodies.Add(new sbody(System.Guid.NewGuid().ToString(), "a"));
            sbodies.Add(new sbody(System.Guid.NewGuid().ToString(), "f"));

            Console.WriteLine(string.Concat(sbodies as IEnumerable<sbody>));

            Console.ReadLine();

            Console.WriteLine(string.Join(',', str.ToArray()));
            Console.ReadLine();




            Persion persion = new Persion("张","三");
            Console.WriteLine(persion.ToString("F"));
            Console.WriteLine($"{persion:L}");
            Console.ReadLine();


            const string text =
            @"This book is perfect for both experienced C# programmers " +
            "looking to sharpen their skills and professional developers who are using C# for " +
            "the first time. The authors deliver unparalleled coverage of Visual Studio 2013 " +
            "and .NET Framework 4.5.1 additions, as well as new test-driven development and " +
            "concurrent programming features. Source code for all the examples are available " +
            "for download, so you can start writing Windows desktop, Windows Store apps, and " +
            "ASP.NET web applications immediately.";


            FindIon(text);

            Console.ReadLine();

           // Regex.Match();


        }

        protected static void FindIon(string text) {

             const string pattern = "ion";
            MatchCollection matches=Regex.Matches(text, pattern, RegexOptions.IgnoreCase|RegexOptions.CultureInvariant);
            WriteMatches(text, matches);

        }


        public static void WriteMatches(string text, MatchCollection matches)
        {
          Console.WriteLine($"Original text was: \n\n{text}\n");
          Console.WriteLine($"No. of matches: {matches.Count}");

            foreach (Match nextMatch in matches)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;

                Console.WriteLine($"Index: {index}, \tString: {result}, \t" +
                $"{text.Substring(index - charsBefore, charsToDisplay)}");
            }
        }


        public class sbody
        {
            public sbody(string id, string name)
            {
                Id = id;
                Name = name;
            }

            public string Id { get; set; }

            public string Name { get; set; }
         
        }
    }
}
