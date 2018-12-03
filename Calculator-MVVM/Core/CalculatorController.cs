using Calculator.MVVM.Events;
using Calculator.MVVM.Pages;
using TimePunch.MVVM.Controller;
using TimePunch.MVVM.EventAggregation;

namespace Calculator.MVVM.Core
{
    public class CalculatorController : BaseController,
        IHandleMessage<NavigateToPage1>,
        IHandleMessage<NavigateToPage2>
    {
        /// <summary>
        /// This method handles the navigation event to page 1
        /// </summary>
        /// <param name="message"></param>
        public void Handle(NavigateToPage1 message)
        {
            NavigateToPage(typeof(Page1View));
        }

        /// <summary>
        /// This method handles the navigation event to page 2
        /// </summary>
        /// <param name="message"></param>
        public void Handle(NavigateToPage2 message)
        {
            NavigateToPage(typeof(Page2View));
        }

        public CalculatorController() : base(CalculatorKernel.Instance.EventAggregator)
        {
        }
    }
}