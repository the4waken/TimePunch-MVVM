using System.Windows.Input;
using TimePunch.MVVM.Events;
using TimePunch.MVVM.ViewModels;
using TimePunchIntroPlayerUWP.Core;

namespace TimePunchIntroPlayerUWP.ViewModels
{
    public class TwitterViewModel : ViewModelBase
    {
        public TwitterViewModel() : base(TimePunchIntroPlayerUWPKernel.Get().EventAggregator)
        {
        }

        public override void Initialize()
        {
            GoBackToMainCommand = RegisterCommand(ExecuteGoBackToMainCommand, CanExecuteGoBackToMainCommand, true);
        }

        public override void InitializePage(object extraData)
        {
        }

        //Properties

        #region GoBackToMainProperties

        public ICommand GoBackToMainCommand
        {
            get => GetPropertyValue(() => GoBackToMainCommand);
            set => SetPropertyValue(() => GoBackToMainCommand, value);
        }

        public void ExecuteGoBackToMainCommand(object sender, ExecutedRoutedEventArgs eventArgs) => EventAggregator.PublishMessage(new GoBackNavigationRequest());

        public void CanExecuteGoBackToMainCommand(object sender, CanExecuteRoutedEventArgs eventArgs) => eventArgs.CanExecute = true;

        #endregion
    }
}
