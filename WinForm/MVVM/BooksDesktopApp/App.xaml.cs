using Contracts.IServices;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;

namespace Contracts
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider ReisterServices()
        {
            var servicecollection= new ServiceCollection();
            servicecollection.AddTransient<BooksViewModel>();
            servicecollection.AddSingleton<IBooksService, BooksService>();
            return servicecollection.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = ReisterServices();
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private BooksService _booksService;

        public BooksService BooksService => _booksService ?? (_booksService = new BooksService(new BooksSampleRepository()));
    }
}
