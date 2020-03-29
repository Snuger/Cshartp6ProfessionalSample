using Calculator.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.ViewModel
{
    public class WindowHeaderViewModel : NotifyCationObject
    {
        public WindowHeaderViewModel()
        {

            CloseWindow = new DelegateCommand();
            CloseWindow.ExecuteCommand = new Action<object>(this.OnCloseWindow);

            MinWindow = new DelegateCommand();
            MinWindow.ExecuteCommand = new Action<object>(this.OnMinWindow);
        }


        public DelegateCommand CloseWindow { get; set; }
        public void OnCloseWindow(object parameter)
        {
            Application.Current.Shutdown();
        }

        public DelegateCommand MinWindow { get; set; }

        public void OnMinWindow(object parameter)
        {          
            Window window = Application.Current.MainWindow;
            window.WindowState = WindowState.Minimized;
        }

    }
}
