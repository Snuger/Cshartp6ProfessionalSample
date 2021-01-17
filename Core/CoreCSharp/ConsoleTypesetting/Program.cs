using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ConsoleTypesetting
{
    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new Dictionary<string, int>()
            {
                ["hammer, ball pein"] = 18,
                ["hammer, cross pein"] = 5,
                ["screwdriver, Phillips #2"] = 14
            };

            Console.WriteLine($"Inventory on {DateTime.Now:d}");
            Console.WriteLine("= = = = = = = = = = = = = = == = = = = = = = = = = = = ");
            Console.WriteLine($"{"Item",-25} {"Quantity",10}");
            foreach (var item in inventory)
                Console.WriteLine($"{item.Key,-25} {item.Value,10} ");
            Console.WriteLine("= = = = = = = = = = = = = = == = = = = = = = = = = = = ");

            Console.WriteLine($"{"Item",-50} {"Quantity",10}");
            foreach (var item in inventory)
                Console.WriteLine($"{item.Key,-50} {item.Value,10} ");



            var tm = (Name: "eggplant", Price: 1.99m, perPackage: 3);
            var date = DateTime.Now;
            Console.WriteLine($"On {date}, the price of {tm.Name} was {tm.Price:C2} per {tm.perPackage} items.");


            var titles = new Dictionary<string, string>()
            {
                ["Doyle, Arthur Conan"] = "Hound of the Baskervilles, The",
                ["London, Jack"] = "Call of the Wild, The",
                ["Shakespeare, William"] = "Tempest, The"
            };

            Console.WriteLine("= = = = = = = = = = = = = = == = = = = = = = = = = = = ");


            Console.WriteLine("Author and Title List");
            Console.WriteLine();
            Console.WriteLine($"|{"Author",-25}|{"Title",30}|");
            foreach (var title in titles)
                Console.WriteLine($"|{title.Key,-25}|{title.Value,30}|");


            var xs = new int[] { 1, 2, 7, 9 };
            var ys = new int[] { 7, 9, 12 };

            // Console.WriteLine($"Find the intersection of the {{{string.Join(", ", xs)}}} and {{{string.Join(", ", ys)}}} sets.");
            Console.WriteLine($"find the intersectin of the {{{string.Join(", ", xs)}}} and {{{string.Join(", ", ys)}}} sets");

            var userName = "Jane";
            var stringWithEscapes = $"C:\\Users\\{userName}\\Documents";
            var verbatimInterpolated = $@"C:\Users\{userName}\Documents";
            Console.WriteLine(stringWithEscapes);
            Console.WriteLine(verbatimInterpolated);


            const int NameAlignment = -9;
            const int ValueAlignment = 7;

            double a = 3;
            double b = 4;
            Console.WriteLine($"Three classical Pythagorean means of {a} and {b}:");
            Console.WriteLine($"|{"Arithmetic",NameAlignment}|{0.5 * (a + b),ValueAlignment:F3}|");
            Console.WriteLine($"|{"Geometric",NameAlignment}|{Math.Sqrt(a * b),ValueAlignment:F3}|");
            Console.WriteLine($"|{"Harmonic",NameAlignment}|{2 / (1 / a + 1 / b),ValueAlignment:F3}|");



            var rand = new Random();
            for (int tt = 0; tt < 7; tt++)
                Console.WriteLine($"Coin flip: {(rand.NextDouble() < 0.5 ? "heads" : "tails")}");


            var cultures = new CultureInfo[] {
                   System.Globalization.CultureInfo.GetCultureInfo("en-US"),
                   System.Globalization.CultureInfo.GetCultureInfo("en-GB"),
                   System.Globalization.CultureInfo.GetCultureInfo("nl-NL"),
                   System.Globalization.CultureInfo.InvariantCulture
            };

            var _date = DateTime.Now;
            var number = 31_415_926.536;
            FormattableString message = $"{_date,20}{number,20:N3}";
            foreach (var culture in cultures)
            {
                var cultureSpecificMessage = message.ToString(culture);
                Console.WriteLine($"{culture.Name,-10}{cultureSpecificMessage}");
            }


            string[] words = new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };

            // Console.WriteLine($"The last word is {words[^1]}");


            (string Alpha, string Beta) sunal = ("a", "b");

            System.Console.WriteLine($"{sunal.Alpha,-10} {sunal.Beta,10}");


            var aplpha = (x: 12, y: 10);
            System.Console.WriteLine($"{aplpha.x,-10}  {aplpha.y,20}");


            (double, int) tt1 = (4.51, 90);

            var t = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            Console.WriteLine(t.Item6);

            (int a, int b) lt = (5, 10);
            (long a, int b) rt = (5, 10);
            Console.WriteLine(lt == rt);


            var dt = new Dictionary<int, (int x, int y, int z)>()
            {
                [1] = (1, 1, 1),
                [2] = (1, 3, 4),
                [3] = (4, 5, 3)
            };

            if (dt.TryGetValue(2, out (int x, int y, int z) outItme))
            {
                Console.WriteLine($"{outItme.x},{outItme.y},{outItme.z}");
            };


            IEnumerable<object> Iab = new List<Object> { 1, 3m, new List<int> { 1, 2, 3, 4, 5, 6 }, new MyClass() { Name = "张三" }, new { x = 10, ys = 12, z = 12 }, 3, "admin" };
            int sum = 0;
            foreach (var item in Iab)
            {
                switch (item)
                {
                    case 0:
                        break;
                    case IEnumerable<int> child:
                        {
                            foreach (var item1 in child)
                                sum += item1 > 0 ? item1 : 0;
                            break;
                        }
                    case int n when n > 0:
                        sum += n;
                        break;
                    case MyClass mys:
                        {
                            Console.WriteLine($"取出object类型------>{mys.Name}");
                            break;
                        }
                }
            }
            Console.WriteLine($"{sum}");


            //调用异步方法
            int result = Program.DoWorkAsync().GetAwaiter().GetResult();
            System.Console.WriteLine($"普通的main方法调用异步方法{result}");

            Console.ReadLine();


            B clab = new B(12);
            Console.WriteLine($"{clab}");

            logger.WriteMessage += LoggingMethods.LogToConsole;
            logger.LogMessage("hello word");


            string extensionTest = "Hello Extension Methods";
            int i = extensionTest.WordCount();



        }


        protected static Task<int> DoWorkAsync()
        {
            return Task.Run(() =>
            {
                return 10;
            });

        }


    }


    public class MyClass
    {
        public string Name { get; set; }

    }
}
