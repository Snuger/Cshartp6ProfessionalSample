using System;
using System.Text;
using System.Linq;

namespace SimpleDelegets
{
    public class Program
    {
        delegate int ReandomCompare(int x, int y);
        private  delegate string GetAsString();
        private delegate string GetTwoString(double value);
        private delegate double Compare(double a, double b);
        private delegate double DoubleOp(double value);




        static void Main(string[] args)
        {
            #region 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.WriteLine("Hello World!");
            ReandomCompare comp = compare;
            for (int p= 5; p>0; p--) {
                DelegetProcess(comp,p, new Random().Next(1, p));
            }


            int pop = 102;
            GetAsString getString = new GetAsString(pop.ToString);
            Console.WriteLine($"方法名:{getString()}");
            Console.WriteLine($"string is{getString.Invoke()}");


            Curreny curreny = new Curreny(99, 334);
            getString = curreny.ToString;
            Console.WriteLine($"方法名: { getString()}");


            getString = new GetAsString(Curreny.GetCurrentUnity);
            Console.WriteLine($"方法名: { getString()}");

            

            getString = () => Curreny.GetDescription();

            Console.WriteLine($"方法名: { getString()}");

          

            Console.WriteLine("Func方法调用完成");
            for (int p = 5; p > 0; p--)
            {
                CompareProcess(CompareDouble, p, new Random().Next(1, p));
            }


            Console.WriteLine("Func方法调用完成");
            Console.ReadLine();



            int[] arg = {1,22,4,3,21,11,24,2,42,18,33 };

            Console.WriteLine("排序前");
            Console.WriteLine(string.Join(",",arg.Select(x=>x.ToString()).ToArray()));

            bool swapped = true;
            do {
                swapped = false;
                for (int t =0;t<arg.Length-1; t++)
                {
                    if (arg[t] > arg[t + 1]) {
                        int p = arg[t];
                        arg[t] = arg[t + 1];
                        arg[t + 1] = p;
                        swapped = true;
                    }  
                 }
            } while (swapped);
            Console.WriteLine("排序后");
            Console.WriteLine(string.Join(",", arg.Select(x => x.ToString()).ToArray()));

            Console.ReadLine();

            Console.WriteLine("测试实例静态方法委托");

            Employee[] employees = new Employee[] {
                 new Employee("张三",20092002),
                 new Employee("李四",1789272),
                 new Employee("王五",1212),
                 new Employee("孙六",2323),
                 new Employee("钱七",32112321),
                 new Employee("刘八",3132132)
            };


            BubbleSorter.Sort<Employee>(employees, Employee.CompareSalary);

            foreach (Employee e in employees) {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();




            Console.WriteLine("测试实例对象委托");

            Employee[] employee = new Employee[] {
                 new Employee("张三",20092002),
                 new Employee("李四",1789272),
                 new Employee("王五",2323232.9m),
                 new Employee("孙六",2323.3m),
                 new Employee("钱七",32112321),
                 new Employee("刘八",3132132),
                 new Employee("三八",223213132)
            };

            Member<Employee> member = new Member<Employee>(employee);
            BubbleSorter.Sort<Employee>(member.Employees, member.CompareSalaryPro);
            foreach (Employee e in member.Employees)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();


            DoubleOp[] doubleOps = {
                MethOperations.MultiplyByTwo,
                MethOperations.Square
            };


            for (int t = 0; t < doubleOps.Length; t++) {

                Console.WriteLine(doubleOps[0].Invoke(99));
                Console.WriteLine(doubleOps[0](93));

                Console.WriteLine(doubleOps[1].Invoke(19));

            }

            double num = 99.0;
            GetTwoString getTwoString = new GetTwoString(GetTwoStringSample);
            //GetTwoString getTwoString = GetTwoStringSample;

            getTwoString(num);
            getTwoString.Invoke(num);

            #endregion


            Func<double, double>[] funcdoubleOps = {
                 MethOperations.MultiplyByTwo,
                 MethOperations.Square
            };
            funcdoubleOps[0].Invoke(99);
            funcdoubleOps[1](892);

            ProcessDeleget(funcdoubleOps[0], 999002);



            int[] sorts = { 1, 32, 31, 3, 21, 13, 4, 22 };
            Console.WriteLine($"排序前：{string.Join(",",sorts.Select(x=>x.ToString()))}");
            Console.ReadLine();
           // Func<int,int,bool> func = comparison;
            Func<int, int, bool> func = (x, y) => x > y;

            bool stop = true;
            do
            {
                stop = false;   
                for (int p = 0; p < sorts.Length - 1; p++)
                {
                    //bool result = func(sorts[p], sorts[p + 1]);
                    //if (result)
                    //{
                    //    int temp = sorts[p];
                    //    sorts[p] = sorts[p + 1];
                    //    sorts[p + 1] = temp;
                    //    stop = true;
                    //}
                    if (sorts[p]>sorts[p + 1])
                    {
                        int temp = sorts[p];
                        sorts[p] = sorts[p + 1];
                        sorts[p + 1] = temp;
                        stop = true;
                    }
                }
            } while (stop);

            Console.WriteLine($"排序后：{string.Join(",", sorts.Select(x => x.ToString()))}");
            Console.ReadLine();

        }

        Func<double, double, bool> GetFunc = delegate (double x, double y) { return x > y; };

        Func<double, double, bool> GetResult = (double x, double y) => x > y;






        static bool comparison(int x, int y) => x > y;


        static void ProcessDeleget(Func<double, double> func, double value) {
            double result = func(value);
            Console.WriteLine($"计算结果:{result}");           

        }


        static void DelegetProcess(ReandomCompare compare, int x, int y) {

            Console.WriteLine($"x+y:{compare(x,y)}");
        }
        static int compare(int x, int y) => x + y;

        static void CompareProcess(Func<double, double, double> acton, double a, double b) {
            double result = acton(a, b);
            Console.WriteLine($"{a}+{b}=>计算结果:{result}");
        }
        static double CompareDouble(double a, double b) => a + b;


        static string GetTwoStringSample(double value) => $"返回结果:{value.ToString()}";



    }
}
