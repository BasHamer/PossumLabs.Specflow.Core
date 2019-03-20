using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using PossumLabs.Specflow.Core.FluidDataCreation;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestJson
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
            Driver = new SetupDriver<Setup>(Setup);
        }

        private Setup Setup { get; set; }
        private SetupDriver<Setup> Driver { get; set; }

        /// <summary>
        /// These are tests that are all the basics
        /// </summary>
        
        [TestMethod]
        public void CreateAParrentObject()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""var"":""P1""}]}");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjects()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""count"":2}]}");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [TestMethod]
        public void CreateAParrentObjectWithACustomName()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""var"":""P1"", ""name"":""Bob""}]}");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Name.Should().Be("Bob");
        }

        [TestMethod]
        public void CreateAParrentObjectWithACustomNameBeforeChildObject()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""var"":""P1"", ""name"":""Bob"", ""ChildObjects"":[{""var"":""C1""}]}]}");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Name.Should().Be("Bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjectsWithACustomNameBeforeChildObjects()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""count"":2, ""name"":""Bob"", ""ChildObjects"":[{""count"":1}]}]}");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Name.Should().Be("Bob");

            DataCreatorFactory.ParrentObjectDataCreator.Store[1].Id.Should().Be(2);
            DataCreatorFactory.ParrentObjectDataCreator.Store[1].Name.Should().Be("Bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [TestMethod]
        public void CreateAParrentObjectWithChildObject()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1""}]}]}");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjectWithChildObjectAccessingData()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1""}]}]}");

            Setup.ParrentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjectsWithACustomNameBeforeChildObjectsAccessingData()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""count"":2, ""name"":""Bob"", ""ChildObjects"":[{""count"":1}]}]}");

            Setup.ParrentObjects["1"].Id.Should().Be(1);
            Setup.ParrentObjects["1"].Name.Should().Be("Bob");

            Setup.ParrentObjects["2"].Id.Should().Be(2);
            Setup.ParrentObjects["2"].Name.Should().Be("Bob");

            Setup.ChildObjects["1"].Id.Should().Be(1);
            Setup.ChildObjects["2"].Id.Should().Be(2);
        }

        /// <summary>
        /// Templates
        /// </summary>

        [TestMethod]
        public void CreateAParrentObjectWithTemplates()
        {
            Driver.Processor(@"{""ParrentObjects"":[{""var"":""P1""},{""var"":""P2"", ""template"":""option1""},{""var"":""P3"", ""template"":""option2""}]}");

            Setup.ParrentObjects["P1"].Value.Should().Be(55);
            Setup.ParrentObjects["P2"].Value.Should().Be(1);
            Setup.ParrentObjects["P3"].Value.Should().Be(2);
        }

        /// <summary>
        /// Object Factory
        /// </summary>


    }
}
