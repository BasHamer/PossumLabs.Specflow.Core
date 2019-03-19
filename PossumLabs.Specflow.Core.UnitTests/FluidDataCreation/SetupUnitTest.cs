using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace PossumLabs.Specflow.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTest
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
        public void CreateAParrentObject()
        {
            Setup.WithParrentObject("P1");

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjects()
        {
            Setup.WithParrentObjects(2);

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [TestMethod]
        public void CreateAParrentObjectWithACustomName()
        {
            Setup.WithParrentObject("P1", configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
            });

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Name.Should().Contain("bob");
        }

        [TestMethod]
        public void CreateAParrentObjectWithACustomNameBeforeChildObject()
        {
            Setup.WithParrentObject("P1", configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
                p.WithChild("C1");
            });

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Name.Should().Contain("bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjectsWithACustomNameBeforeChildObjects()
        {
            Setup.WithParrentObjects(2, configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
                p.WithChilderen(1);
            });

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Name.Should().Contain("bob");

            DataCreatorFactory.ParrentObjectDataCreator.Store[1].Id.Should().Be(2);
            DataCreatorFactory.ParrentObjectDataCreator.Store[1].Name.Should().Contain("bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void CreateAParrentObjectWithACustomNameAfterChildObject()
        {
            Setup.WithParrentObject("P1", configurer: p =>
            {
                p.WithChild("C1");
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
            });
        }

        [TestMethod]
        public void CreateAParrentObjectWithChildObject()
        {
            Setup.WithParrentObject("P1", configurer: p =>
            {
                p.WithChild("C1");
            });

            DataCreatorFactory.ParrentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParrentObjectDataCreator.Store[0].Id.Should().Be(1);

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        //    [TestMethod]
        //public void Junk()
        //{
        //    Setup.WithParrentObject("P1", p =>
        //    {
        //        p.WithChildObject("C1", c => c.Name = "Bob's ChildObject") // configuring a value
        //                p.WithChildObject("C2");
        //        p.Name = "Bob's ParrentObject"; //throws an error "the ParrentObject has already been created, please move this call before any With calls"
        //    });
        //    var c2ChildObjectId = Setup.ChildObjects["C2"].ChildObjectId //returns a simple object, the construction (WithXXX) methods would not exist on this.
        //}
    }
}
