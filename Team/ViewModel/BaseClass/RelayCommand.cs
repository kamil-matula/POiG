// Copyrights: Adam Zielonka
using System;
using System.Windows.Input;

namespace TeamMVVM.ViewModel.BaseClass
{
    class RelayCommand : ICommand // klasa imitująca polecenie
    {
        readonly Action<object> _execute;         // delegata metod nic nie zwracających o 1 argumencie t. object
        readonly Predicate<object> _canExecute;   // delegata metod zwracających bool o argumencie t. object

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            else _execute = execute;
            _canExecute = canExecute;
        }

        // możliwość wykonania polecenia:
        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);
        public event EventHandler CanExecuteChanged
        {
            add { if (_canExecute != null) CommandManager.RequerySuggested += value; }    // dołączanie metody do zdarzenia
            remove { if (_canExecute != null) CommandManager.RequerySuggested -= value; } // usuwanie metody ze zdarzenia
        }

        //wykonanie polecenia:
        public void Execute(object parameter) { _execute(parameter); }
    }
}