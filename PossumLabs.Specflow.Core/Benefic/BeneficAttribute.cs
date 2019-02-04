using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.Benefic
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BeneficAttribute : Attribute
    {
        public BeneficAttribute(string name = null, string icon = null, string patternOverwrite = null, bool discoverable = true, int order = 0)
        {
            Name = name;
            Icon = icon;
            PatternOverwrite = patternOverwrite;
            Discoverable = discoverable;
            Order = order;
        }
        public string Name { get; }
        public string Icon { get; }
        public string PatternOverwrite { get; }
        public bool Discoverable { get; }
        public int Order { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BeneficDefaultsAttribute : Attribute
    {
        public BeneficDefaultsAttribute(string icon=null, bool discoverable = true, int orderSeed = 0)
        {
            Icon = icon;
            Discoverable = discoverable;
        }
        public string Icon { get; }
        public bool Discoverable { get; }
        public int OrderSeed { get; }
    }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InputAttribute : Attribute
    {
        public InputAttribute(string name = null, string format = null)
        {
            Name = name;
            Format = format;
        }
        public string Name { get; }
        public string Format { get; }
    }
}





