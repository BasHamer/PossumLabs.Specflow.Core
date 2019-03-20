using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestTemplates
    {
        public DataCreatorFactory DataCreatorFactory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            DataCreatorFactory = new DataCreatorFactory();
            var factory = new PossumLabs.Specflow.Core.Variables.ObjectFactory();
            var interperter = new PossumLabs.Specflow.Core.Variables.Interpeter(factory);
            var templateManager = new PossumLabs.Specflow.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());

            Setup = new Setup(DataCreatorFactory, factory, templateManager, interperter);
        }

        private Setup Setup { get; set; }

        [TestMethod]
        public void CreateAParrentObjectWithTemplates()
        {
            Setup.WithParrentObject("P1");
            Setup.WithParrentObject("P2", "option1");
            Setup.WithParrentObject("P3", "option2");

            Setup.ParrentObjects["P1"].Value.Should().Be(55);
            Setup.ParrentObjects["P2"].Value.Should().Be(1);
            Setup.ParrentObjects["P3"].Value.Should().Be(2);
        }
    }
}
