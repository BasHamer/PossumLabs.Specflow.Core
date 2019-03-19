using System;
using PossumLabs.Specflow.Core.Variables;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    public interface IDomainObjectSetupBase<T, Tid>
        where T : IDomainObject
        where Tid : IEquatable<Tid>
    {

        Tid GetId(T item);
    }
}