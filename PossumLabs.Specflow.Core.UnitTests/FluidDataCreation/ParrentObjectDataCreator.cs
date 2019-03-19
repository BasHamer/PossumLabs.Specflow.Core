using PossumLabs.Specflow.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ParrentObjectDataCreator : IDataCreator<ParrentObject>
    {
        public ParrentObjectDataCreator()
        {
            Store = new List<ParrentObject>();
        }
        public ParrentObject Create(ParrentObject item)
        {
            lock(Store)
            {
                Store.Add(item);
                item.Id = Store.Count;
            }
            return item;
        }

        public List<ParrentObject> Store { get; }
    }
}
