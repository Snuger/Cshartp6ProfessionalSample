using CrazyElemphant.Client.Contracts.IServices;
using CrazyElemphant.Client.Reposetory;
using CrazyElemphant.Client.Services;
using CrazyElemphant.Client.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CrazyElemphant.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Container { get; private set; }

        private IServiceProvider RegisterService()
        {
            var servicecollection = new ServiceCollection();
            servicecollection.AddTransient<DishViewModel>();
            servicecollection.AddSingleton<IDishService, DishService>();
            return  servicecollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e) {
            Container = RegisterService();
            base.OnStartup(e);
        }

        private DishService _dishService;

        public DishService GetDishService => _dishService ?? (_dishService = new DishService(new DishRespostry()));


    }
}
