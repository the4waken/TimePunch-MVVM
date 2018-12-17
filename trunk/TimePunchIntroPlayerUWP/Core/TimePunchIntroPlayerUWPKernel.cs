using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimePunch.MVVM.EventAggregation;

namespace TimePunchIntroPlayerUWP.Core
{
    public class TimePunchIntroPlayerUWPKernel
    {
        private static TimePunchIntroPlayerUWPKernel IntroPlayerUWP;

        private TimePunchIntroPlayerUWPKernel()
        {
            EventAggregator = new EventAggregator();
        }

        public static TimePunchIntroPlayerUWPKernel Get()
        {
            if (IntroPlayerUWP == null)
            {
                IntroPlayerUWP = new TimePunchIntroPlayerUWPKernel();
                return IntroPlayerUWP;
            }
            return IntroPlayerUWP;
        }

        public IEventAggregator EventAggregator;
    }
}
