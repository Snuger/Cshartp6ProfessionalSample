using Contracts;
using Contracts.IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class BooksService : IBooksService
    {
        private IBookRepository _booksRespository;
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public BooksService(IBookRepository booksRespository)
        {
            _booksRespository = booksRespository;
        }

        public IEnumerable<Book> Books => _books;

        public async Task<Book> AddOrUpdateBookAsync(Book book)
        {
            Book updated = null;
            if (book.BookId == 0)
            {
                updated = await _booksRespository.AddAsync(book);
                _books.Add(updated);
            }
            else {
                updated = await _booksRespository.UpdateAsync(book);
                Book old = _books.Where(c => c.BookId == updated.BookId).Single();
                int inx = _books.IndexOf(old);
                _books.RemoveAt(inx);
                _books.Insert(inx, updated);
            }
            return updated;
        }

        public Book GetBook(int bookId) => _books.Where(c => c.BookId == bookId).SingleOrDefault();


        public async Task LoadBooksAsync()
        {
            if (_books.Count > 0) return;

            IEnumerable<Book> books = await _booksRespository.GetItemsAsync();
            _books.Clear();
            foreach (var b in books) {
                _books.Add(b);
            }
        }
    }
}
