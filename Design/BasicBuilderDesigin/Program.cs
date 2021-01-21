using System;
using System.Text.Json;
using System.Threading;

namespace BasicBuilderDesigin
{
    class Program
    {
        static void Main(string[] args)
        {   
            Manufacturer chery = new Manufacturer(new CheryBuilder());
            for (var t = 0; t < 10; t++) {
                Car car = chery.Delivery();
                Thread.Sleep(500);
                Console.WriteLine($"{JsonSerializer.Serialize<Car>(car)}");
              //  Console.WriteLine($"{System.DateTime.Now}，交付了第{t}台：{car.Brand},装配:{car.Engine.Displacement}排量发动机,发动机号:{car.Engine.EngineNum,10}");
            }
            Console.ReadLine();
        }
    }
}
