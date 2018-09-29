using System;
using System.Text;
namespace ArraySample
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            


            // Console.WriteLine("Hello World!");

            int[] a = new int[4];

            int[] p = { 2, 3, 4, 5, 6, 7, 7, 77 };


            int[] q = new int[] { 1, 23, 4, 5, 33, 322, 3 };


            int[,] ars = new int[3, 4];
            ars[0, 0] = 12;

            int[,] arg = { { 1, 2, 3, 3 }, { 3, 4, 5, 3 }, { 1, 12, 32, 1 }, { 11, 23, 2, 3 } };


            Persion[] persion = new Persion[1];
            persion[0] = new Persion() { FirstName = "张", LastName = "三" };


            Persion[] _persion = {
              new Persion(){ FirstName="李",LastName="四"},
              new Persion(){ FirstName="王",LastName="五"}
            };

            int[,,] dom = {
                {{0,1},{1,2},{2,3}},
                {{1,4},{2,5},{3,6}},
                {{2,7},{3,8},{4,9}}
            };

            Console.WriteLine(dom[2, 1, 1]);
            Console.ReadLine();


            int[][] joid = new int[3][];

            joid[0] = new int[3] { 1, 2, 3 };
            joid[1] = new int[5] { 12, 3, 2, 44, 3 };
            joid[2] = new int[2] { 1, 2 };


            Array array = Array.CreateInstance(typeof(int), 4);
            for (int i = 0; i < array.Length; i++)
            {
                array.SetValue(new Random().Next(0, 1000), i);
            }

            for (int ad = 0; ad < array.Length; ad++)
            {
                Console.WriteLine(array.GetValue(ad));
            }



            int[] array1 = (int[])array;


            Array races = Array.CreateInstance(typeof(Persion), new int[] { 2, 4 });
            races.SetValue(new Persion() { FirstName = "0", LastName = "1" }, 0, 0);
            races.SetValue(new Persion() { FirstName = "0", LastName = "2" }, 0, 1);
            races.SetValue(new Persion() { FirstName = "0", LastName = "3" }, 0, 2);
            races.SetValue(new Persion() { FirstName = "0", LastName = "4" }, 0, 3);
            races.SetValue(new Persion() { FirstName = "1", LastName = "1" }, 1, 0);
            races.SetValue(new Persion() { FirstName = "1", LastName = "2" }, 1, 1);
            races.SetValue(new Persion() { FirstName = "1", LastName = "3" }, 1, 2);
            races.SetValue(new Persion() { FirstName = "1", LastName = "4" }, 1, 3);
            Console.ReadLine();

             var current = races.GetEnumerator();
            while (current.MoveNext()) {
                Persion pn = (Persion)current.Current;
                Console.WriteLine($"{pn.FirstName}-=-{pn.LastName}");
            }
            

            Persion[,] persionClone = (Persion[,])races.Clone();
            Console.WriteLine(persionClone[0, 2].ToString());
            Console.ReadLine();


            Persion[] bealtes = {
                new Persion(){ FirstName="fast",LastName="admin" },
                new Persion(){ FirstName="last",LastName="conrt" }
            };
           
            Persion[] bealtesClone =(Persion[]) bealtes.Clone();



            int[] sorttest = {2,3,21,23,2,2,1,133,444,22,11 };

            Console.WriteLine("排序前：");

            foreach (int t in sorttest) {
                Console.Write($"{t},");

            }
            Console.WriteLine();
            Console.WriteLine("排序后：");
            Console.WriteLine("==============");
            Console.WriteLine();

            Array.Sort(sorttest);
            foreach (int t in sorttest)
            {
                Console.Write($"{t},");
            }
            Console.WriteLine();
            Console.WriteLine("==============");
            Console.WriteLine();
            Console.WriteLine("以下演示ArraySegment用法");


            int[] arr1 = { 1,2,3,4,5,6,7,8}; ;
            int[] arr2 = { 11,22,33,44,55,66,77,99};

            var sements = new ArraySegment<int>[]{
                new ArraySegment<int>(arr1,0,1),
                new ArraySegment<int>(arr2,0,1)
            };

            int sum = SumOfArraySegment(sements);

            Console.WriteLine($"计算总额如下:{sum}");

            Console.ReadLine();

        }



        public static int SumOfArraySegment(ArraySegment<int>[] segments)
        {
            int sum = 0;

            foreach (var segment in segments) {

                for (int p = 0; p < segment.Offset+segment.Count; p++) {
                    sum += segment.Array[p];
                }
            }

            return sum;
        }
    }
}
