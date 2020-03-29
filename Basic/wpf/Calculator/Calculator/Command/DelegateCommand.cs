using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.Command
{
   public  class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<object> ExecuteCommand { get; set; }

        public Func<object,bool> CanExecuteCommand { get; set; }


        public bool CanExecute(object parameter)
        {
            if(CanExecuteCommand!= null)
            {
                return CanExecuteCommand(parameter);
            }
            return true;           
        }

        public void Execute(object parameter)
        {
            if (ExecuteCommand != null)
            {
                ExecuteCommand(parameter);
            }          
        }
    }
}
