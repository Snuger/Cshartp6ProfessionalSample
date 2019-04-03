using Contracts.IServices;
using FrameWork;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels
{
    public class BooksViewModel:ViewModelBase,IDisposable
    {
        private IBooksService _booksService;

        public ICommand GetBooksCommand { get; }

        public ICommand AddBookCommond { get; }

        private bool _canGetBooks = true;

        public BooksViewModel(IBooksService booksService)
        {
            _booksService = booksService;
            GetBooksCommand = new DelegateCommand(OnGetBooks, CanGetBooks);
            AddBookCommond = new DelegateCommand(OnAddBook);

        }

        public IEnumerable<Book> Books => _booksService.Books;


        public Book Book { get; set; }

        public async void OnGetBooks()
        {
            await _booksService.LoadBooksAsync();
            _canGetBooks = false;
            (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        }

        public async void OnAddBook()
        {
            await _booksService.AddOrUpdateBookAsync(Book);
            _canGetBooks = true;
        }

        public bool CanGetBooks() => _canGetBooks;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
