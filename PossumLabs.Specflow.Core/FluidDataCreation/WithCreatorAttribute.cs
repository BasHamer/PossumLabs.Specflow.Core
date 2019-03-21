using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class WithCreatorAttribute : Attribute
    {
        public WithCreatorAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
