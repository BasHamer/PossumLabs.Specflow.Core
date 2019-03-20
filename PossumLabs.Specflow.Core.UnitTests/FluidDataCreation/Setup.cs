using PossumLabs.Specflow.Core.FluidDataCreation;
using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    public class Setup:SetupBase<Setup>
    {
        public Setup(
           IDataCreatorFactory dataCreatorFactory,
           ObjectFactory objectFactory,
           TemplateManager templateManager,
           Interpeter interpeter) : base(dataCreatorFactory, objectFactory, templateManager, interpeter)
        {

            ObjectFactory.Register<ParrentObject>((f) =>
            {
                var i = new ParrentObject();
                i.ComplexValue = ObjectFactory.CreateInstance<ValueObject>();
                return i;
            });

            ObjectFactory.Register<ChildObject>((f) =>
            {
                var i = new ChildObject();
                i.ComplexValue = ObjectFactory.CreateInstance<ValueObject>();
                return i;
            });

            ParrentObjects = new RepositoryBase<ParrentObject>(Interpeter, ObjectFactory);
            ChildObjects = new RepositoryBase<ChildObject>(Interpeter, ObjectFactory);

            Interpeter.Register(ParrentObjects);
            Interpeter.Register(ChildObjects);
        }

        public RepositoryBase<ParrentObject> ParrentObjects { get; }
        public RepositoryBase<ChildObject> ChildObjects { get; }

        public Setup WithParrentObject(string name, string template = null, Action<ParrentObjectSetup> configurer = null)
            => With<ParrentObject,ParrentObjectSetup,int>(ParrentObjects, name, template, configurer);

        public Setup WithParrentObjects(int count, string template = null, Action<ParrentObjectSetup> configurer = null)
            => WithMany<ParrentObject, ParrentObjectSetup, int>(ParrentObjects, WithParrentObject, count, template, configurer);

        public Setup WithChildObject(string name, string template = null, Action<ChildObjectSetup> configurer = null)
            => With<ChildObject, ChildObjectSetup, int>(ChildObjects, name, template, configurer);

        public Setup WithChildObjects(int count, string template = null, Action<ChildObjectSetup> configurer = null)
            => WithMany<ChildObject, ChildObjectSetup, int>(ChildObjects, WithChildObject, count, template, configurer);
    }
}
