﻿using PossumLabs.Specflow.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ValueObjectSetup : ValueObjectSetupBase<ValueObject>
    {
        public string Name
        {
            get { return Item.Name; }
            set
            {
                Item.Name = value;
                NotifyChange();
            }
        }

        public int Value
        {
            get { return Item.Value; }
            set
            {
                Item.Value = value;
                NotifyChange();
            }
        }
    }
}
