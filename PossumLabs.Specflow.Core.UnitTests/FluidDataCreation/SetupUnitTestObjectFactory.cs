using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestObjectFactory
    {
        public DataCreatorFactory DataCreatorFactory { get; set; }
        public PossumLabs.Specflow.Core.Variables.ObjectFactory Factory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            DataCreatorFactory = new DataCreatorFactory();
            Factory = new PossumLabs.Specflow.Core.Variables.ObjectFactory();
            var interpeter = new PossumLabs.Specflow.Core.Variables.Interpeter(Factory);
            var templateManager = new PossumLabs.Specflow.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Setup = new Setup(DataCreatorFactory, Factory, templateManager, interpeter);

            new PossumLabs.Specflow.Core.Variables.ExistingDataManager(interpeter).Initialize(Assembly.GetExecutingAssembly());
        }

        private Setup Setup { get; set; }


        [TestMethod]
        public void MakeSureExistingDataIsLoaded()
        {
            Factory.Register<ValueObject>(o => new ValueObject { Name = "special" });

            Setup.WithParentObject("P1", configurer: p=>p.ComplexValue.Value=42);

            Setup.ParentObjects["P1"].ComplexValue.Value.Should().Be(42);
            Setup.ParentObjects["P1"].ComplexValue.Name.Should().Be("special");
            
        }
    }
}
