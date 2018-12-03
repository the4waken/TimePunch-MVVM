using System.Windows.Input;
using Calculator.MVVM.Core;
using TimePunch.MVVM.Events;
using TimePunch.MVVM.ViewModels;

namespace Calculator.MVVM.ViewModels
{
    public class CalculatorViewmodel : ViewModelBase
    {
        #region Overrides of ViewModelBase

        public override void Initialize()
        {
            ButtonClickCommand = RegisterCommand(ExecuteButtonClickCommand, CanExecuteButtonClickCommand, true);
        }

        public override void InitializePage(object extraData)
        {
        }

        #endregion

        #region ButtonClick Command

        /// <summary>
        /// Gets or sets the ButtonClick command.
        /// </summary>
        /// <value>The ButtonClick command.</value>
        public ICommand ButtonClickCommand
        {
            get { return GetPropertyValue(() => ButtonClickCommand); }
            set { SetPropertyValue(() => ButtonClickCommand, value); }
        }

        /// <summary>
        /// Determines whether this instance can execute ButtonClick command.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance can execute ButtonClick command; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event arguments</param>
        public void CanExecuteButtonClickCommand(object sender, CanExecuteRoutedEventArgs eventArgs)
        {
            eventArgs.CanExecute = true;
        }

        /// <summary>
        /// Executes the ButtonClick command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event arguments</param>
        public void ExecuteButtonClickCommand(object sender, ExecutedRoutedEventArgs eventArgs)
        {
        }

        #endregion

        public CalculatorViewmodel() 
            : base(CalculatorKernel.Instance.EventAggregator)
        {
        }
    }
}
