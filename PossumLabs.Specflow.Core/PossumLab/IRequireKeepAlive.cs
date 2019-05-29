using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.PossumLab
{
    public interface IRequireKeepAlive
    {
        int KeepAlivePeriodInMs { get; }
        Action KeepAlive { get; }
    }
}
