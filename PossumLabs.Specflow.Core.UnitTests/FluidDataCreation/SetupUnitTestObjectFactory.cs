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

        [TestInitialize]
        public void Initialize()
        {
            DataCreatorFactory = new DataCreatorFactory();
            var factory = new PossumLabs.Specflow.Core.Variables.ObjectFactory();
            var interpeter = new PossumLabs.Specflow.Core.Variables.Interpeter(factory);
            var templateManager = new PossumLabs.Specflow.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Setup = new Setup(DataCreatorFactory, factory, templateManager, interpeter);

            new PossumLabs.Specflow.Core.Variables.ExistingDataManager(interpeter).Initialize(Assembly.GetExecutingAssembly());
        }

        private Setup Setup { get; set; }


        [TestMethod]
        public void MakeSureExistingDataIsLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
