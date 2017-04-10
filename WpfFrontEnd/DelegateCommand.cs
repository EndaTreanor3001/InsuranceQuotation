using System;
using System.Windows.Input;

namespace WpfFrontEnd
{
    public class DelegateCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canLaunch;

        public DelegateCommand(Action action, Func<bool> canLaunch)
        {
            _action = action;
            _canLaunch = canLaunch;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
