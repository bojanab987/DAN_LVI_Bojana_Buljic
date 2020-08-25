using System;
using System.Windows.Input;

namespace DAN_LVI_Bojana_Buljic.Command
{
    public class RelayCommand:ICommand
    {
        #region Fields readonly
        Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion

        #region Constructors
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members[DebuggerStepThrough]
        /// <summary>
        /// Method for check if command should be executed
        /// </summary>
        /// <param name="parameter">object parameter</param>
        /// <returns>true or false</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Do the functionality of executing command
        /// </summary>
        /// <param name="parameter">object parameter</param>
        public void Execute(object parameter) { _execute(parameter); }
        #endregion
    }
}
