using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core
{
    public class DatetimeManager
    {
        public DatetimeManager(Func<DateTime> now)
        {
            Now = now;
        }
        public Func<DateTime> Now { get; }
    }
}
