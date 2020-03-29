using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            List<string> resultList = new List<string>();
            List<List<string>> zipList = new List<List<string>>() {
                new List<string>(){"a","b","c","d","e"},
                new List<string>(){"1","2","3","4","5"},
                new List<string>(){"A","B","C","D","E"},
                new List<string>(){"一","二","三","四","五"}
            };
            zipList.ForEach(item =>
            {                
                if (resultList.Count == 0) {
                    resultList = item;
                }
                else {
                    resultList = resultList.Zip(item, (first, second) => first + second).ToList(); 
                }            
            });

            Console.WriteLine(string.Join(",", resultList.ToArray()));
            Console.ReadLine();


            var products = new List<Product>
{
            new Product {Id = 1, Category = "Electronics", Value = 15.0},
            new Product {Id = 2, Category = "Groceries", Value = 40.0},
            new Product {Id = 3, Category = "Garden", Value = 210.3},
            new Product {Id = 4, Category = "Pets", Value = 2.1},
            new Product {Id = 5, Category = "Electronics", Value = 19.95},
            new Product {Id = 6, Category = "Pets", Value = 21.25},
            new Product {Id = 7, Category = "Pets", Value = 5.50},
            new Product {Id = 8, Category = "Garden", Value = 13.0},
            new Product {Id = 9, Category = "Automotive", Value = 10.0},
            new Product {Id = 10, Category = "Electronics", Value = 250.0},
            };

            var query = (from r in products select r).ToLookup(r => r.Category);
            foreach (var group in query) {

                Console.WriteLine(group.Key);

                foreach (var item in group) {
                    Console.WriteLine(item);                   
                }
            }
            Console.ReadLine();


         var values= Enumerable.Range(1, 20);
         Console.WriteLine(string.Join(",", values.ToArray()));

         Console.ReadLine();


            string[] names1 = { "Hartono, Tommy" };

            string[] names2 = { "Adams, Terry", "Andersen, Henriette Thaulow",
                      "Hedlund, Magnus", "Ito, Shu" };

            string[] names3 = { "Solanki, Ajay", "Hoeing, Helge",
                      "Andersen, Henriette Thaulow",
                      "Potra, Cristina", "Iallo, Lucio" };

            List<string[]> namesList =  new List<string[]> { names1, names2, names3 };

            var querys = namesList.Aggregate(Enumerable.Empty<string>(), (first, second) => second.Length > 3 ? first.Union(second) : first);

            foreach (string name in querys)
            {
                Console.WriteLine(name);
            }
            Console.ReadLine();


            Console.WriteLine("测试linq并开");
            List<int> data = SampleData();
            Console.WriteLine("数据构造完成");
            var res = (from r in data.AsParallel() where Math.Log(r) < 4 select r).ToList().Average();
            Console.WriteLine($"计算结果:{res}");
            Console.ReadLine();

        }


        static List<int> SampleData() {
            int arraylisSize = 500000000;
            Random random = new Random();
            return Enumerable.Range(0, arraylisSize).Select(x => random.Next(110)).ToList();
        }




    }
}
