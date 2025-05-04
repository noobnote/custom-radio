using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp10.Commads
{
    public class RelayCommand : ICommand
    {
        public Func<object,bool> CanExecuteFunc { get; set; }
        public Action<object> ExecuteAction { get; set; }
        public RelayCommand(Func<object, bool> canExecuteFunc, Action<object> executeAction)
        {
            if (canExecuteFunc == null || executeAction == null) throw new ArgumentNullException();


            this.CanExecuteFunc = canExecuteFunc;
            this.ExecuteAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }
    }
}
