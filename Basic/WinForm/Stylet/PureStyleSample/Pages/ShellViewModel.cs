using System;
using PureStyleSample.ViewModel;
using Stylet;

namespace PureStyleSample.Pages
{
    public class ShellViewModel : Screen
    {

        private readonly FirstViewModel _firstViewModel = new FirstViewModel();

        private readonly SecondViewModel secondViewModel = new SecondViewModel();


        private string _name = "我们的祖国是花园";
     
        public string Name
        {
            get { return _name; }
            set {
                SetAndNotify(ref _name, value);            
            }
        }

        private object currentViewModel;

        public ShellViewModel()
        {
            CurrentViewModel = _firstViewModel;
        }

        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetAndNotify(ref this.currentViewModel, value);
            }
        }
    }
}
