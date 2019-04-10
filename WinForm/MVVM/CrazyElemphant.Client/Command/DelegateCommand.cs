using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrazyElemphant.Client.Command
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action executeCommand, Func<object, bool> canExecuteCommand)
        {
            if (executeCommand == null)
                throw new ArgumentNullException(nameof(executeCommand));
            ExecuteCommand = executeCommand;
            CanExecuteCommand = canExecuteCommand;
        }

        public DelegateCommand(Action executeCommand)
            :this(executeCommand,null)
        {
        }

        public event EventHandler CanExecuteChanged;

        public Action ExecuteCommand { get; set; }

        public Func<object, bool> CanExecuteCommand { get; set; }


        public bool CanExecute(object parameter)
        {
            if(CanExecuteCommand!=null)            
                return  CanExecuteCommand(parameter);    
            return true;
        }

        public void Execute(object parameter)
        {
            if (ExecuteCommand != null)
                ExecuteCommand();       
        }
    }
}
