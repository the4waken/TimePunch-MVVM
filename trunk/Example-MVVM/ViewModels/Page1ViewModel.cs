﻿using System.Windows.Input;
using Example.MVVM.Core;
using TimePunch.MVVM.Events;
using TimePunch.MVVM.ViewModels;

namespace Example.MVVM.ViewModels
{
    public class Page1ViewModel : ViewModelBase
    {
        #region Overrides of ViewModelBase

        /// <summary>
        /// Initializes the ViewModel. 
        /// 
        /// This is used to handle initialization that can't be done in the constructor.
        /// The method should only called once, after the ViewModel has been created.
        /// </summary>
        public override void Initialize()
        {
            GoBackCommand = RegisterCommand(ExecuteGoBackCommand, CanExecuteGoBackCommand, true);
        }

        /// <summary>
        /// Initializes the Page.
        /// 
        /// This method is used to do some page initialization. 
        /// The calling page can start the new page with some extra data for page initialization.
        /// 
        /// This method is also called on a GoBackNavigationRequest, but without parameter data.
        /// </summary>
        /// <param name="extraData"></param>
        public override void InitializePage(object extraData)
        {
        }

        #endregion

        #region GoBack Command

        /// <summary>
        /// Gets or sets the GoBack command.
        /// </summary>
        /// <value>The GoBack command.</value>
        public ICommand GoBackCommand
        {
            get { return GetPropertyValue(() => GoBackCommand); }
            set { SetPropertyValue(() => GoBackCommand, value); }
        }

        /// <summary>
        /// Determines whether this instance can execute GoBack command.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance can execute GoBack command; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">Event arguments</param>
        public void CanExecuteGoBackCommand(object sender, CanExecuteRoutedEventArgs eventArgs)
        {
            eventArgs.CanExecute = true;
        }

        /// <summary>
        /// Executes the GoBack command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">Event arguments</param>
        public void ExecuteGoBackCommand(object sender, ExecutedRoutedEventArgs eventArgs)
        {
            EventAggregator.PublishMessage(new GoBackNavigationRequest());
        }

        #endregion

        public Page1ViewModel() 
            : base(ExampleKernel.Instance.EventAggregator)
        {
        }
    }
}
