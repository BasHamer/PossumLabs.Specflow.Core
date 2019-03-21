using System;
using System.Collections.Generic;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    internal class IgnoreCaseEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
            => x.Equals(y, StringComparison.InvariantCultureIgnoreCase);

        public int GetHashCode(string obj)
            => obj.ToUpper().GetHashCode();
    }
}