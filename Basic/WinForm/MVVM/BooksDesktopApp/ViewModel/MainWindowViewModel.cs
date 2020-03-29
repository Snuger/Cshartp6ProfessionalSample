using BooksDesktopApp.API;
using FrameWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BooksDesktopApp.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private Page currentPage=PageManager.PageHome;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                this.RaisePropertyChanged("CurrentPage");
            }
        }

        private LeftMenu selectMenu;

        public LeftMenu SelectMeun
        {
            get { return selectMenu; }
            set
            {
                selectMenu = value;
                switch (selectMenu)
                {
                    case LeftMenu.Home:
                        CurrentPage = PageManager.PageHome;
                        break;
                    case LeftMenu.Collection:
                        CurrentPage = PageManager.PageCollection;
                        break;
                }
               this.RaisePropertyChanged("SelectMeun");
            }
        }

    }
}
