using PossumLabs.Specflow.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ParentObjectDataCreator : IDataCreator<ParentObject>
    {
        public ParentObjectDataCreator()
        {
            Store = new List<ParentObject>();
        }
        public ParentObject Create(ParentObject item)
        {
            lock(Store)
            {
                Store.Add(item);
                item.Id = Store.Count;
            }
            return item;
        }

        public List<ParentObject> Store { get; }
    }
}
