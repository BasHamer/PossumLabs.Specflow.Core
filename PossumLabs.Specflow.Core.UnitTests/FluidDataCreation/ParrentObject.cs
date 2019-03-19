using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ParrentObject:IDomainObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public ValueObject ComplexValue { get; set; }
        public ChildObject Child { get; set; }
        public string ParrentObjectId { get; internal set; }

        public string LogFormat()
        {
            throw new NotImplementedException();
        }
    }
}
