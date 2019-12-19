using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.RepositoryDefaults
{
    [TestClass]
    public class DefaultTests
    {
        public ObjectFactory ObjectFactory { get; set; }
        public Interpeter Interpeter { get; set; }
        public SubDivisionRepository SubDivisionRepository { get; set; }
        public DivisionRepository DivisionRepository { get; set; }
        public CompanyRepository CompanyRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
            SubDivisionRepository = new SubDivisionRepository(Interpeter, ObjectFactory);
            DivisionRepository = new DivisionRepository(Interpeter, ObjectFactory);
            CompanyRepository = new CompanyRepository(Interpeter, ObjectFactory);
        }

        [TestMethod]
        public void NothingToDo()
        {
        }

        [TestMethod]
        public void OneLayer()
        {
        }

        [TestMethod]
        public void MultiLayer()
        {
        }

        [TestMethod]
        public void PreventRepeatCalls()
        {
        }

        [TestMethod]
        public void MultiLayerPreventRepeatCalls()
        {
        }
    }
}
