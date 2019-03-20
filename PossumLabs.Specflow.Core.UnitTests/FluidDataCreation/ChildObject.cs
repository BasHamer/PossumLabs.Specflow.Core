using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    
    public class ChildObject : IDomainObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
        public ValueObject ComplexValue { get; set; }
        public ParrentObject ParrentObject { get; set; }

        public string LogFormat()
        {
            throw new NotImplementedException();
        }
    }
}
