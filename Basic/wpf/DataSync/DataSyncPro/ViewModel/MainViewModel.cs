using GalaSoft.MvvmLight;
using DataSyncPro.Model;
using MaterialDesignThemes.Wpf;
using System.Collections;
using System.Collections.Generic;
using DataSyncPro.Contract;
using DataSyncPro.Contract.IService;

namespace DataSyncPro.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {      
        private readonly ISpecialMenuService _specialMenuService;

   
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ISpecialMenuService specialMenuService)
        {
            _specialMenuService = specialMenuService;
            _specialMenuService.GetBasicMenu((item, error) =>
            {
                if (error != null)
                {
                    return;
                }

                BasicMenus = item;
            });      
        }

        private IEnumerable<BasicMenu> _basicMenus;

        public IEnumerable<BasicMenu> BasicMenus
        {
            get { return _basicMenus; }
            set { Set(ref _basicMenus, value); }
        }



        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}