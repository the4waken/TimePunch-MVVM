// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using TimePunch.MVVM.Events;

namespace TimePunch.MVVM.Commands
{
    public class RelayCommand : ICommand
    {
        private static readonly ConstructorInfo ExecutedRoutedEventArgsConstructor =
            typeof(ExecutedRoutedEventArgs).GetConstructors(
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).First();

        private static readonly ConstructorInfo CanExecuteRoutedEventArgsConstructor =
            typeof(CanExecuteRoutedEventArgs).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).First();

        /// <summary>
        ///     Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object, ExecutedRoutedEventArgs> execute,
            Action<object, CanExecuteRoutedEventArgs> canExecute)
        {
            ExecuteAction = execute;
            CanExecuteAction = canExecute;

            if (execute != null)
                OwnsExecute = true;

            // get hold of dispatcher
            if (Application.Current != null)
                CurrentDispatcher = CoreApplication.GetCurrentView()?.CoreWindow?.Dispatcher
                                    ?? Window.Current?.Dispatcher;
        }

        /// <summary>
        ///     Gets or sets the execute action.
        /// </summary>
        /// <value>The execute action.</value>
        protected Action<object, ExecutedRoutedEventArgs> ExecuteAction { get; }

        /// <summary>
        ///     Gets or sets the can execute predicate.
        /// </summary>
        /// <value>The can execute predicate.</value>
        protected Action<object, CanExecuteRoutedEventArgs> CanExecuteAction { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether [owns execute].
        /// </summary>
        /// <value><c>true</c> if [owns execute]; otherwise, <c>false</c>.</value>
        protected bool OwnsExecute { get; }

        /// <summary>
        ///     Gets or sets the current dispatcher.
        /// </summary>
        /// <value>The current dispatcher.</value>
        protected CoreDispatcher CurrentDispatcher { get; }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            if (ExecuteAction == null)
                return;

            var args = new ExecutedRoutedEventArgs(this, parameter);
            ExecuteAction(parameter, args);
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction == null)
                return false;

            var args = new CanExecuteRoutedEventArgs(this, parameter);

            CanExecuteAction(parameter, args);
            return args.CanExecute;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler == null)
                return;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            if (CurrentDispatcher != null && !CurrentDispatcher.HasThreadAccess)
                CurrentDispatcher.RunAsync(CoreDispatcherPriority.Normal, RaiseCanExecuteChanged);
            else
                handler(this, EventArgs.Empty); // already on the UI thread
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}