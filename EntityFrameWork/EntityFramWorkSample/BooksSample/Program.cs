using System;
using System.Threading.Tasks;
using System.Linq;

namespace BooksSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            Program program = new Program();
            program.AddBookAsync("上下五千年", "易中天").Wait();
            program.AddBooksAsync().Wait();
            program.ReadBooks();
            program.QueryBooks();
            program.UpdateBookAsync().Wait();
            Console.ReadLine();
        }


        private async Task AddBookAsync(string title, string publisher) {

            using (BooksContext context = new BooksContext()) {

                Book book = new Book
                {
                    Title = title,
                    Publisher = publisher
                };

                context.Add(book);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} record added");
            }           
        }

        private async Task AddBooksAsync()
        {

            using (BooksContext context = new BooksContext())
            {

                Book book = new Book
                {
                    Title = "Professional C# 5 and .NET 4.5.1",
                    Publisher = "Wrox Press"
                };

                Book book1 = new Book
                {
                    Title = "Professional C# 2012 and .NET 4.5",
                    Publisher = "Wrox Press"
                };

                Book book2 = new Book
                {
                    Title = "JavaScript for Kids",
                    Publisher = "Wrox Press"
                };

                Book book3 = new Book
                {
                    Title = "Web Design with HTML and CSS",
                    Publisher = "Wrox Press"
                };

                context.AddRange(book, book1, book2, book3);
               // context.Add(book);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} record added");
            }
           
        }

        protected void ReadBooks() {
            using (BooksContext context = new BooksContext()) {
                var books=context.Books;
                foreach (Book book in books) {
                    Console.WriteLine($"{book.BookId}\t{book.Title}\t\t\t{book.Publisher}");
                }

            }
        }


        protected void QueryBooks() {
            using (BooksContext context = new BooksContext()) {
                var books = context.Books.Where(p => p.Publisher == "Wrox Press");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("查找结果:");
                foreach (Book book in books) {
                    Console.WriteLine($"{book.BookId}\t {book.Title} \t {book.Publisher}");
                }


            }
        }

        private async Task UpdateBookAsync() {

            int records = 0;
            using (BooksContext context = new BooksContext()) {
               
                var book = context.Books.Where(c => c.Title == "上下五千年").FirstOrDefault();
                if (book != null) {
                    book.Title = "Up and down 5000 years";
                    book.Publisher = "Mr e zhon Tian";
                   records= await context.SaveChangesAsync();
                }
            }
            Console.WriteLine($"{records} record updated ");
        }
    }
}
