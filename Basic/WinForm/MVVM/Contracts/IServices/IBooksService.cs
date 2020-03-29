using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IServices
{
    public interface IBooksService
    {

        Task LoadBooksAsync();

        IEnumerable<Book> Books { get; }

        Book GetBook(int bookId);

        Task<Book> AddOrUpdateBookAsync(Book book);
    }
}
