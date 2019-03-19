using PossumLabs.Specflow.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ChildObjectDataCreator : IDataCreator<ChildObject>
    {
        public ChildObjectDataCreator()
        {
            Store = new List<ChildObject>();
        }
        public ChildObject Create(ChildObject item)
        {
            lock (Store)
            {
                Store.Add(item);
                item.Id = Store.Count;
            }
            return item;
        }

        public List<ChildObject> Store { get; }
    }
}
