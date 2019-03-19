using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    public abstract class DomainObjectSetupBase<T, Tid> : ValueObjectSetupBase<T>, IDomainObjectSetupBase<T, Tid> where T : IDomainObject
        where Tid : IEquatable<Tid>
    {
        public DomainObjectSetupBase(T item, Func<T, Tid> creator): base(item)
        {
            Creator = creator;
            ActualId = new Lazy<Tid>(Create);
        }

        public DomainObjectSetupBase()
        {

        }

        protected override void NotifyChange([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (ActualId.IsValueCreated)
                throw new Exception($"can't set the value of {propertyName} after the item is created in storage.");
            InvokePropertyChanged(propertyName);
        }

        public T Final
        {
            get
            {
                if (ActualId.IsValueCreated || IdIsSupplied)
                    return Item;
                SuppliedId = Create();
                return Item;
            }
        }

        private Func<T, Tid> Creator { get; set; }
        private Tid Create() => Creator(Item);
        private Lazy<Tid> ActualId { get; set; }
        private Tid SuppliedId { get; set; }

        public abstract Tid GetId(T item);

        private bool IdIsSupplied => SuppliedId != null && !SuppliedId.Equals((Tid)Activator.CreateInstance(typeof(Tid)));

        public Tid Id
        {
            get => IdIsSupplied ? SuppliedId : ActualId.Value;
            set
            {
                SuppliedId = value;
                InvokePropertyChanged("Id");
            }
        }
    }
}
