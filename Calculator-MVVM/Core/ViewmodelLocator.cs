using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.MVVM.ViewModels;

namespace Calculator.MVVM.Core
{
    public class ViewModelLocator
    {
        private CalculatorViewmodel calculatorViewmodelBacking = null;
        private ExampleViewModel exampleViewModelBacking = null;
        private Page1ViewModel page1ViewModelBacking = null;
        private Page2ViewModel page2ViewModelBacking = null;

        public CalculatorViewmodel CalculatorViewModel
        {
            get
            {
                calculatorViewmodelBacking?.Dispose();
                calculatorViewmodelBacking = new CalculatorViewmodel();
                calculatorViewmodelBacking.Initialize();
                return calculatorViewmodelBacking;
            }
        }

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
