using Example.MVVM.ViewModels;

namespace Example.MVVM.Core
{
    public class ViewModelLocator
    {
        private static ExampleViewModel exampleViewModelBacking = null;
        private static Page1ViewModel page1ViewModelBacking = null;
        private static Page2ViewModel page2ViewModelBacking = null;

        public ExampleViewModel ExampleViewModel
        {
            get
            {
                exampleViewModelBacking?.Dispose();
                exampleViewModelBacking = new ExampleViewModel();
                exampleViewModelBacking.Initialize();
                return exampleViewModelBacking;
            }
        }

        public Page2ViewModel Page2ViewModel
        {
            get
            {
                page2ViewModelBacking?.Dispose();
                page2ViewModelBacking = new Page2ViewModel();
                page2ViewModelBacking.Initialize();
                return page2ViewModelBacking;
            }
        }

        public Page1ViewModel Page1ViewModel
        {
            get
            {
                page1ViewModelBacking?.Dispose();
                page1ViewModelBacking = new Page1ViewModel();
                page1ViewModelBacking.Initialize();
                return page1ViewModelBacking;
            }
        }
    }
}
