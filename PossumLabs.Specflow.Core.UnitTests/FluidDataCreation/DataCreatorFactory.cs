using PossumLabs.Specflow.Core.FluidDataCreation;
using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class DataCreatorFactory : IDataCreatorFactory
    {
        public DataCreatorFactory()
        {
            ParrentObjectDataCreator = new ParrentObjectDataCreator();
            ChildObjectDataCreator = new ChildObjectDataCreator();
        }
        public ParrentObjectDataCreator ParrentObjectDataCreator { get; }
        public ChildObjectDataCreator ChildObjectDataCreator { get; }

        public IDataCreator<T> GetCreator<T>() where T : IDomainObject
        {
            if (typeof(T) == typeof(ParrentObject))
                return ParrentObjectDataCreator as IDataCreator<T>;
            if (typeof(T) == typeof(ChildObject))
                return ChildObjectDataCreator as IDataCreator<T>;
            throw new NotImplementedException();
        }
    }
}
