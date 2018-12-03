using TimePunch.MVVM.EventAggregation;

namespace Calculator.MVVM.Core
{
    public class CalculatorKernel
    {
        private static CalculatorKernel instance;

        private CalculatorKernel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets or sets the Kernel Instance
        /// </summary>
        public static CalculatorKernel Instance
        {
            get
            {
                if (instance == null)
                    instance = new CalculatorKernel(new EventAggregator());

                return instance;
            }
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <returns></returns>
        public IEventAggregator EventAggregator { get; }
    }
}
