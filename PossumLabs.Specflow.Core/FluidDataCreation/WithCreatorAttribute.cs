﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class WithCreatorAttribute : CreatorAttribute
    {
        public WithCreatorAttribute(string name) : base(name)
        { }
    }
}
