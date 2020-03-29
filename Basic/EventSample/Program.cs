using System;
using System.Text;

namespace EventSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            CarDealer dealer = new CarDealer();
            Consumer consumer = new Consumer("张三");
           
           

          //  WeakEventManger<CarDealer, CarInfoEventArgs>.AddHandler(dealer, "newCarInfo", consumer.NewCarIsHere);



            dealer.newCarEventHander += consumer.NewCarIsHere;

            dealer.NewCar("兰博基尼");



            dealer.NewCar("雪夫兰");
            var sesstion = new Consumer("王五");
            dealer.newCarEventHander += sesstion.NewCarIsHere;

            dealer.NewCar("别克");
            dealer.newCarEventHander -= sesstion.NewCarIsHere;
            


            Console.ReadLine();
        }
      
    }
}
