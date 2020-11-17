using System;
using System.Text;

namespace HelloWordApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.WriteLine(Encoding.GetEncoding("GB2312"));
            /*
            var name = "杨靖";           
            Console.WriteLine($"姓名{name}");
            Console.WriteLine($"name的类型{name.GetType()}");
            //return t;
            */

            //此时myNumber是不能用new 初始化的
            //  myNumber number = new myNumber(123);
            //Console.Write(number.)

            //SingLetn singet= SingLetn.Intance;

            // Console.WriteLine(singet.ToString());


            Car car = new Car("法拉利");

            Console.WriteLine(car.ToString());

            car = new Car(2, "1.0");
            Console.WriteLine(car.ToString());



            sheep sheep = new sheep("123");

            sheep sheep1 = new sheep("456");


            WorkDay day = new WorkDay();


            UserType userType = (UserType)3;
            Console.WriteLine($"{userType.ToString()}");

            UserType types = UserType.HIS | UserType.HIS;

            Console.WriteLine($"{types.ToString()}");
        }

    }

    public class myNumber
    {

        private static int _num;
        private myNumber(int number)
        {
            _num = number;
        }
    }


    public class SingLetn
    {

        private static SingLetn _intance;

        private int _state;
        private SingLetn(int state)
        {
            _state = state;
        }
        public static SingLetn Intance
        {

            get { return _intance ?? (_intance = new SingLetn(999)); }
        }

        public override string ToString()
        {
            return _state.ToString();
        }



    }


    public class Car
    {
        private string _description;
        private uint _nWheels;
        private string _displacement;




        public Car(string description, uint wheels, string displacement = "1.5T")
        {

            _description = description;
            _nWheels = wheels;
            _displacement = displacement;
        }

        #region 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /*public Car(string description) {
            _description = description;
            _nWheels = 4;
        }*/
        #endregion
        public Car(string description) : this(description, 4)
        {

        }

        public Car(uint wheels, string displacement) : this("摩托车", wheels, displacement) { }


        public override string ToString()
        {
            return $"{System.DateTime.Now.ToString()},工厂造出一辆{_nWheels}轮子的{_description};";
        }

    }


    public class sheep
    {
        private static int num = 0;
        private string _name;


        public sheep()
        {
            //
        }

        /// <summary>
        /// 些方法只执行一次
        /// </summary>
        static sheep()
        {
            num += 1;
            Console.WriteLine($"第{num}次初始化");
        }

        public sheep(string name)
        {
            _name = name;
            Console.WriteLine($"啦啦啦....{_name}");
        }
    }


    public class WorkDay
    {
        private static ConsoleColor backColoer;

        static WorkDay()
        {
            DateTime timeNow = new DateTime();

            switch (timeNow.DayOfWeek)
            {

                case DayOfWeek.Friday:
                    backColoer = ConsoleColor.Cyan;
                    break;
                case DayOfWeek.Monday:
                    backColoer = ConsoleColor.Red;
                    break;
                case DayOfWeek.Saturday:
                    backColoer = ConsoleColor.DarkCyan;
                    break;
                case DayOfWeek.Sunday:
                    backColoer = ConsoleColor.DarkGray;
                    break;
                case DayOfWeek.Thursday:
                    backColoer = ConsoleColor.DarkGreen;
                    break;
                case DayOfWeek.Tuesday:
                    backColoer = ConsoleColor.DarkMagenta;
                    break;
                case DayOfWeek.Wednesday:
                    backColoer = ConsoleColor.DarkRed;
                    break;
            }
        }

        public WorkDay()
        {
            DateTime timeNow = new DateTime();
            Console.ForegroundColor = backColoer;
            Console.WriteLine($"当前时间是{timeNow.ToString("yyyy-MM-DD hh:ssmm")},今天是{timeNow.DayOfWeek}");

        }

    }
}
