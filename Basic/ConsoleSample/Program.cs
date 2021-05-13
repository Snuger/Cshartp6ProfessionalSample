using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Action<string> BookAction = new Action<string>(bookName => Console.WriteLine($"我买的书是:{bookName}"));
            BookAction.Invoke("百年孤独");

            Func<string, Book> Buy = new Func<string, Book>(bookName =>
            {
                return new Book(bookName) { Unit = 20, Quantity = 1 };
            });
            var result = Buy.Invoke("百年孤独");
            Console.WriteLine($"{result.BookName}   {result.Quantity}   {result.Unit}    {result.Subtotal}");
            Orders _order = new Orders();
            Func<string> funcValue = () => "我是即将传递的值3";
            DisplayValue(funcValue);
            _order.Add(() => "百年孤独");
            _order.Add(() => "霸道少爷");
            Console.WriteLine($"合    计   {_order.Books.Sum(c => c.Quantity)}   --    {_order.Books.Sum(c => c.Subtotal)}");

            Console.WriteLine(_order.Books.ToJson());

            Console.WriteLine(_order.ToJson());
        }

        private static void DisplayValue(Func<string> func)
        {
            string message = func.Invoke();
            Console.WriteLine($"收到消息如下:{message}");

        }
    }

    class Orders
    {
        public List<Book> Books = new List<Book>();

        public void Add(Func<string> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));
            var book = new Book(func.Invoke()) { Unit = new Random().Next(1, 100), Quantity = 1 };
            Console.WriteLine($"{book.BookName}   {book.Quantity}   {book.Unit}    {book.Subtotal}");
            this.Books.Add(book);
        }

    }

    class Book
    {
        public Book(string bookName)
        {
            this.BookName = bookName;
        }

        public string BookName { get; set; }

        public double Unit { get; set; }

        public int Quantity { get; set; }

        public double Subtotal { get => Unit * Quantity; }
    }

}
