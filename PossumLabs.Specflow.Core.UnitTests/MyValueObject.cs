using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests
{
    class MyValueObject : IValueObject
    {
        public string MyString { get; set; }
        public int MyInt { get; set; }
        public int? MyNullableInt { get; set; }
        public List<string> MyStringList { get; set; }
        public int[] MyIntArray { get; set; }

        public MyValueObject NestedValueObject { get; set; }
    }
}
