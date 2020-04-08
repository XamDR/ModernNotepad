using System;
using System.Windows.Input;

namespace ModernNotepadLibrary.Helpers
{
    /// <summary>
    /// A class that implements the <see cref="ICommand"/> interface.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Func<object, bool> canExecuteAction;

        /// <summary>
        /// Initializes an instances of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="action">An action to execute. Takes a parameter of type <see cref="object"/>.</param>
        /// <param name="canExecuteAction">Sets if the action can be executed. Takes a parameter of type <see cref="object"/>.</param>
        public DelegateCommand(Action<object> action, Func<object, bool> canExecuteAction)
        {
            this.action = action;
            this.canExecuteAction = canExecuteAction;            
        }        

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="action">An action to execute.</param>
        /// <param name="canExecuteAction">Sets if the action can be executed.</param>
        public DelegateCommand(Action action, Func<bool> canExecuteAction) : this(o => action(), o => canExecuteAction()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="action">An action to execute. Takes a parameter of type <see cref="object"/>.</param>
        public DelegateCommand(Action<object> action) : this(action, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="action">An action to execute</param>
        public DelegateCommand(Action action) : this(o => action()) { }

        /// <summary>
        /// Determines whether this instance can execute the specified action.
        /// </summary>
        /// <param name="parameter">The parameter sent to the action.</param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => canExecuteAction == null ? true : canExecuteAction(parameter);

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <param name="parameter">The parameter sent to the action.</param>
        public void Execute(object parameter) => action(parameter);

        /// <summary>
        /// Raises the can execute changed event.
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler CanExecuteChanged;
    }
}
