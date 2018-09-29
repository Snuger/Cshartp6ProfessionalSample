using System;
using System.Collections.Generic;
using System.Text;
using Variance.CollectionsFuncion;
using Variance.Specification;

namespace Variance
{
    class Program
    {
        static void Main(string[] args)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            //协变
            IIndex<Rectangle> rectangles = RectangleCollection.GetRectangleCollection();
            IIndex<Shape> shape = rectangles;
            for (int t = 0; t < shape.Count; t++) {
                Console.WriteLine(shape[t].ToString());
            }


            //抗變
            IDisplay<Shape> display = new ShapeDisplay();
            IDisplay<Rectangle> rectangleDisplay = display;
            rectangleDisplay.Show(rectangles[0]);



            Console.ReadLine();




            Console.WriteLine("以下演示泛型类型的静态变量！！");
            MyClass<int> class1 = new MyClass<int>();
            MyClass<int> class12 = new MyClass<int>();
            Console.WriteLine(class1.Count);
            Console.WriteLine(class12.Count);


            MyClass<double> class2 = new MyClass<double>();
            Console.WriteLine(class2.Count);



            Console.WriteLine("以下演示泛型方法 ！！");

            var result= Max<int>(10, 20);
            Console.WriteLine(result.ToString());


            Console.WriteLine("类型继承泛型类型 ！！");


            MyClass myClass = new MyClass();
            MyClass myClass2 = new MyClass();
            Console.WriteLine(myClass.Count);



            Console.ReadLine();




            var Acountes = new List<Account>() {
                new Account("张三", 10022),
                new Account("张三", 10022),
                 new Account("张三", 10022),
                  new Account("张三", 10022),
                   new Account("张三", 10022),
                    new Account("张三", 10022)
            };

           decimal amount=Algorithm.AcclumateSample(Acountes);
            Console.WriteLine(amount);



            decimal amount2 = Algorithm.Acclumate<Account>(Acountes);
            Console.WriteLine(amount2);



            var deposites = new List<DepositAccount>()
            {
                new DepositAccount(1000,"sngue.smae")
            };

            decimal deposits = Algorithm.Acclumate<DepositAccount>(deposites);
            Console.WriteLine(deposits);



            decimal count = Algorithm.Acclumate<Account, decimal>(Acountes, (item, sum) => sum + item.Balance);

            Console.WriteLine(count);


            //  decimal DepositAccount



            MethoedOverLoad methoed = new MethoedOverLoad();
            methoed.Foo(33);
            methoed.Foo("admin");
            methoed.Foo("ddfdasf", "dfdsafds");
            methoed.Foo(983, "你朱工");

            Console.WriteLine("==========================================");

            methoed.Bar("三人行，必有我师也");


            methoed.Bar(3343);

         

            Console.ReadLine();

        }


        public  static T Max<T>(T op1, T op2) where T:IComparable
        {
            if (op1.CompareTo(op2) < 0)
                return op1;
            return op2;
        }
    }
}
