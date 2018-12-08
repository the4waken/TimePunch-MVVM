using TimePunch.MVVM.EventAggregation;

namespace Example.MVVM.Core
{
    public class ExampleKernel
    {
        private static ExampleKernel instance;

        private ExampleKernel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets or sets the Kernel Instance
        /// </summary>
        public static ExampleKernel Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExampleKernel(new EventAggregator());

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
