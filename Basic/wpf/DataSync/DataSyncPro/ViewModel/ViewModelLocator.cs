/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:DataSyncPro.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using DataSyncPro.Model;
using DataSyncPro.Contract;
using CommonServiceLocator;
using DataSyncPro.IService;
using DataSyncPro.Contract.IService;
using DataSyncPro.Contract.IRepository;
using DataSyncPro.Repository;
using DataSyncPro.Services;
using System.Collections.ObjectModel;
using DataSyncPro.Db;
using System.Collections.Generic;

namespace DataSyncPro.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDataService, DataService>();

            SimpleIoc.Default.Register<ISpecialMenuService, SpecialMenuService>();
            SimpleIoc.Default.Register<IDataBaseRepository, DataBaseRepository>();
            SimpleIoc.Default.Register<IDataBaseService, DataBaseService>();          

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AutomaticUploadViewModel>();
            SimpleIoc.Default.Register<DataBaeConfigViewModel>();
            SimpleIoc.Default.Register<DataBaseGatherViewModel>();
            SimpleIoc.Default.Register<DataSourceSettinsViewModel>();
            SimpleIoc.Default.Register<TargetDataBaeSettingsViewModel>();
        
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }      
        public AutomaticUploadViewModel AutomaticUploadViewModel { get => ServiceLocator.Current.GetInstance<AutomaticUploadViewModel>(); }


        public DataBaeConfigViewModel DataBaeConfigViewModel { get => ServiceLocator.Current.GetInstance<DataBaeConfigViewModel>(); }


        public DataBaseGatherViewModel DataBaseGatherViewModel { get => ServiceLocator.Current.GetInstance<DataBaseGatherViewModel>(); }


        public DataSourceSettinsViewModel DataSourceSettinsViewModel { get => ServiceLocator.Current.GetInstance<DataSourceSettinsViewModel>(); }


        public TargetDataBaeSettingsViewModel TargetDataBaeSettingsViewModel { get => ServiceLocator.Current.GetInstance<TargetDataBaeSettingsViewModel>(); }
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}