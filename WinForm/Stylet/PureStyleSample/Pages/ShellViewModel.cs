using System;
using Stylet;

namespace PureStyleSample.Pages
{
    public class ShellViewModel : Screen
    {
        private IWindowManager windowManager;

        private string _name = "我们的祖国是花园";

        public ShellViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        public string Name
        {
            get { return _name; }
            set {
                SetAndNotify(ref _name, value);
                this.NotifyOfPropertyChange(() => this.CanSayHello);
            }
        }

        public void SayHello() {
            Name = $"{Name},花园的花朵真鲜艳.";
            windowManager.ShowMessageBox(this.Name);
        }


        public bool CanSayHello {
            get => !string.IsNullOrEmpty(Name);
        }
       
    }
}
