using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.Specflow.Core.Variables;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.Variables
{
    [TestClass]
    public class InterperterUnitTest
    {
        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }
        private TestTypeRepository Repository { get; }

        public InterperterUnitTest()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
            Repository = new TestTypeRepository( Interpeter, ObjectFactory);
            Interpeter.Register(Repository);
        }

        public class TestType : IValueObject {
            public string a { get; set; }
        }

        public class ParrentType : TestType
        {
            public string a { get; set; }
        }

        public class TestTypeRepository : RepositoryBase<TestType>
        {
            public TestTypeRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
            {
            }
        }

        public class ParrentTypeRepository : RepositoryBase<ParrentType>
        {
            public ParrentTypeRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
            {
            }
        }

        [TestMethod]
        public void Simple()
        {
            Repository.Add("k1", new TestType { a = "test" });
            Interpeter.Get<TestType>("k1").a
                .Should().Be("test");
        }

        [TestMethod]
        public void SimplePath()
        {
            Repository.Add("k1", new TestType { a = "test" });
            Interpeter.Get<string>("k1.a")
                .Should().Be("test");
        }

        [TestMethod]
        public void SimpleDownCast()
        {
            Repository.Add("k1", new TestType { a = "test" });
            ((TestType)Interpeter.Get<object>("k1")).a
                .Should().Be("test");
        }

        [TestMethod]
        public void OneLevelDownCast()
        {
            var r = new ParrentTypeRepository(Interpeter, ObjectFactory);
            Interpeter.Register(r);
            r.Add("k1", new ParrentType { a = "test" });
            ((ParrentType)Interpeter.Get<TestType>("k1")).a
                .Should().Be("test");
        }
    }
}
