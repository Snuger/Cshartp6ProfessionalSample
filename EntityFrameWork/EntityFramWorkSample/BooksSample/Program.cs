using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            program.ConflictHandlingAsync().Wait();
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
                    book.Title = "Conflict Handling";
                    book.Publisher = "Mr e zhon Tian";
                   records= await context.SaveChangesAsync();
                }
            }
            Console.WriteLine($"{records} record updated ");
        }

        private async Task ConflictHandlingAsync() {
            // user 1
            Tuple<BooksContext, Book> tuple1 = await PrepareUpdateAsync();
            tuple1.Item2.Title = "user 1 wins";

            // user 2
            Tuple<BooksContext, Book> tuple2 = await PrepareUpdateAsync();
            tuple2.Item2.Title = "user 2 wins";


            // user 1
            await UpdateAsync(tuple1.Item1, tuple1.Item2,"user1");
            // user 2
            await UpdateAsync(tuple2.Item1, tuple2.Item2,"user2");


            tuple1.Item1.Dispose();
            tuple2.Item1.Dispose();

            await CheckUpdateAsync(tuple1.Item2.BookId);

        }

        private static async Task<Tuple<BooksContext, Book>> PrepareUpdateAsync() {
            var context = new BooksContext();
            Book book = await context.Books.Where(b => b.Title == "Conflict Handling").FirstOrDefaultAsync();
            return Tuple.Create(context, book);
        }

        private static async Task UpdateAsync(BooksContext context, Book book,string user) {

            try
            {

                Console.WriteLine($"{user}: updating id {book.BookId}, " +$"timestamp: {book.TimeStamp}");
                ShowChanges(book.BookId, context.Entry(book));
                int records=await context.SaveChangesAsync();
                Console.WriteLine($"{user}: updated {book.TimeStamp}");
                Console.WriteLine($"{user}: {records} record(s) updated while updating " +  $"{book.Title}");


            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"{user}: update failed with {book.Title}");
                Console.WriteLine($"error: {ex.Message}");
                foreach (var entry in ex.Entries)
                {
                    Book b = entry.Entity as Book;
                    Console.WriteLine($"{b.Title} {b.TimeStamp}");
                    ShowChanges(book.BookId, context.Entry(book));
                }
            }




            Console.WriteLine($"successfully written to the database: id {book.BookId} with title {book.Title}");
        }

        private static async Task CheckUpdateAsync(int id) {
            using (var context=new BooksContext())
            {
                Book book = await context.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
                Console.WriteLine($"updated  {book.Title}");

            }
        }


        private static void ShowChanges(int id, EntityEntry entity)
        {
            ShowChange(id, entity.Property("Title"));
            ShowChange(id, entity.Property("Publisher"));
        }

        private static void ShowChange(int id, PropertyEntry propertyEntry) {

            Console.WriteLine($"id: {id}, current: {propertyEntry.CurrentValue}, " +$"original: {propertyEntry.OriginalValue}, " +$"modified: {propertyEntry.IsModified}");
        }
    }
}
