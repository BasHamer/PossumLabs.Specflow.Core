﻿using System;
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

        [TestMethod]
        public void CreateAParrentObjectWithChildObjectAccessingData()
        {
            Setup.WithParrentObject("P1", configurer: p =>
            {
                p.WithChild("C1");
            });

            Setup.ParrentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParrentObjectsWithACustomNameBeforeChildObjectsAccessingData()
        {
            Setup.WithParrentObjects(2, configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
                p.WithChilderen(1);
            });

            Setup.ParrentObjects["1"].Id.Should().Be(1);
            Setup.ParrentObjects["1"].Name.Should().Contain("bob");

            Setup.ParrentObjects["2"].Name.Should().Contain("bob");
            
            Setup.ChildObjects["1"].Id.Should().Be(1);
            Setup.ChildObjects["2"].Id.Should().Be(2);
        }
    }
}
