﻿using Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class BooksSampleRepository : IBookRepository
    {
        private List<Book> _books;

        public BooksSampleRepository()
        {
            InitSampleBooks();
        }

        private void InitSampleBooks() {
            _books = new List<Book>()
            {
           new Book { BookId = 1, Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press" },
                new Book { BookId = 2, Title = "Professional C# 5.0 and .NET 4.5.1", Publisher = "Wrox Press" },
                new Book { BookId = 3, Title = "Enterprise Services with the .NET Framework", Publisher = "AWL" }
            };
        }

        public Task<Book> AddAsync(Book item)
        {           
            item.BookId = _books.Select(b => b.BookId).Max() + 1;
            _books.Add(item);
            return Task.FromResult(item);           
        }

        public Task<bool> DelteAsync(int id)
        {
            Book bookToDelete = _books.Find(b => b.BookId == id);
            if (bookToDelete != null) {
                return Task.FromResult<bool>(_books.Remove(bookToDelete));
            }
            return Task.FromResult<bool>(false);
        }

        public Task<Book> GetItemAsync(int id)=>Task.FromResult(_books.Find(c => c.BookId == id));



        public Task<IEnumerable<Book>> GetItemsAsync() => Task.FromResult<IEnumerable<Book>>(_books);

        public Task<Book> UpdateAsync(Book item)
        {
            Book bookToUpdate = _books.Find(c => c.BookId == item.BookId);
            int ix = _books.IndexOf(bookToUpdate);
            _books[ix] = item;
            return Task.FromResult(_books[ix]);
        }
    }
}
