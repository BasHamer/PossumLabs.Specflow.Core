using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.Logging
{
    public interface ILogFormatter
    {
        string Format(string section, object data);
    }
}