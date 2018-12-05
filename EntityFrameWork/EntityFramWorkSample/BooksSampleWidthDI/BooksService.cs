using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BooksSampleWidthDI
{
    public class BooksService
    {
        private readonly BooksContext booksContext;

        public BooksService(BooksContext booksContext)
        {
            this.booksContext = booksContext;
        }

        public async Task AddBooksAsync() {

            var b1 = new Book { Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press" };
            var b2 = new Book { Title = "Professional C# 2012 and .NET 4.5", Publisher = "Wrox Press" };
            var b3 = new Book { Title = "JavaScript for Kids", Publisher = "Wrox Press" };
            var b4 = new Book { Title = "Web Design with HTML and CSS", Publisher = "For Dummies" };
            booksContext.AddRange(b1, b2, b3, b4);
            int records = await booksContext.SaveChangesAsync();           
        }


        public void ReadBooks() {

            var books = booksContext.Books.Where(o => o.Title.Contains("五千"));
            foreach (var book in books) {
                Console.WriteLine($"{book.Title} {book.Publisher}");
            }
            Console.WriteLine("=====================================");           
        }
    }
}
