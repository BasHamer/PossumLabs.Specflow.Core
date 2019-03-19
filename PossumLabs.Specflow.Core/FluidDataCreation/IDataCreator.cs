using System;
using System.Collections.Generic;
using System.Text;
using PossumLabs.Specflow.Core.Variables;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    public interface IDataCreator<T> where T: IDomainObject
    {
        T Create(T item);
    }
}
