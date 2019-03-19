using System;
using System.Collections.Generic;
using System.Text;
using PossumLabs.Specflow.Core.Variables;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    public interface IDataCreatorFactory
    {
        IDataCreator<T> GetCreator<T>() where T : IDomainObject;
    }
}
