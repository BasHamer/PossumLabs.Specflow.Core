﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.Variables
{
    public interface IEntity :IValueObject
    {
        string LogFormat();
    }
}
