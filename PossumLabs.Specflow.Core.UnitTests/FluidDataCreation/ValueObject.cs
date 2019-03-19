using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ValueObject:IValueObject
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
