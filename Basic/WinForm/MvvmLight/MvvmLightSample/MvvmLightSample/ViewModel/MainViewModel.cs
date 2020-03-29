using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightSample.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace MvvmLightSample.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Title = "MaterialDesignThemes(MvvmLight)基本使用示例";
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            this.Cartes = new List<Carte>() {
               new Carte(){ Name="默认",Page=PageManager.PageWellcome},
               new Carte(){ Name="边框", Page=PageManager.BorderSamples },
               new Carte(){ Name="文本框",Page=PageManager.TextBoxSamples},
               new Carte(){ Name="ListBox Sample",Page=PageManager.ListBoxSample}
            };
            MenuSelectChangeCommand= new RelayCommand(MenuSelected);
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }

        public List<Carte> Cartes { get; set; }

        private Page _currentPage=PageManager.PageWellcome;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { Set(ref _currentPage, value); }
        }

        public ICommand MenuSelectChangeCommand { get; set; }

        public void MenuSelected()
        {
            Title = "测试成功与否";
        }
    }
}   