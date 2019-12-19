using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.Variables
{
    /// <summary>
    /// Same as DefaultToRepositoryDefault, acurate C# vocabulary
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NullCoalesceWithDefaultAttribute : Attribute
    {
    }

    /// <summary>
    /// Same as NullCoalesceWithDefault, better English Version
    /// </summary>
    public class DefaultToRepositoryDefaultAttribute : NullCoalesceWithDefaultAttribute
    {
    }
}
