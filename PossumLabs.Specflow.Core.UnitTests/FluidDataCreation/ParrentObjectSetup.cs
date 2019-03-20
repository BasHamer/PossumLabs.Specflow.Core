using PossumLabs.Specflow.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class ParrentObjectSetup : DomainObjectSetupBase<ParrentObject, int>
    {
        public ParrentObjectSetup WithChild(string name, string template = null, Action<ChildObjectSetup> configurer = null)
        {
            Setup.WithChildObject(name, template, child => {
                child.ParrentObjectId = this.Id; // important
                child.ParrentObject = this.Item;
                configurer?.Invoke(child);
            });
            return this;
        }

        public ParrentObjectSetup WithChilderen(int count, string template = null, Action<ChildObjectSetup> configurer = null)
        {
            Setup.WithChildObjects(count, template, child => {
                child.ParrentObjectId = this.Id; // important
                child.ParrentObject = this.Item;
                configurer?.Invoke(child);
            });
            return this;
        }

        public override int GetId(ParrentObject item)
            => item.Id;

        public string Name
        {
            get { return Item.Name; }
            set
            {
                Item.Name = value;
                NotifyChange();
            }
        }

        public string Category
        {
            get { return Item.Category; }
            set
            {
                Item.Category = value;
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

        private ValueObjectSetup _ComplexValue;
        public ValueObjectSetup ComplexValue
        {
            get { return _ComplexValue; }
            set
            {
                Unsubscribe(_ComplexValue);
                _ComplexValue = value;
                Subscribe(_ComplexValue);
                NotifyChange();
            }
        }

        private Setup Setup { get; set; }
        protected override void SetSetup(ISetup setupBase)
            => Setup = setupBase as Setup;
    }
}
