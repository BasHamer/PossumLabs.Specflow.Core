using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.Specflow.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.Logging
{
    [TestClass]
    public class YamlLogFormatterTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Target = new YamlLogFormatter();
        }

        public YamlLogFormatter Target { get; set; }

        [TestMethod]
        public void Simple()
        {
            var ret = Target.Format("bob", 42);
            ret.Should().Contain("section: bob");
            ret.Should().Contain("data: 42");
        }

        [TestMethod]
        public void SimpleNullSection()
        {
            var ret = Target.Format(null, 42);
            ret.Should().Contain("section:");
            ret.Should().Contain("data: 42");
        }

        [TestMethod]
        public void SimpleNullData()
        {
            var ret = Target.Format("bob", null);
            ret.Should().Contain("section: bob");
            ret.Should().Contain("data:");
        }

        [TestMethod]
        public void SimpleListData()
        {
            var ret = Target.Format(null, new List<int> { 42, 43 });
            ret.Should().Contain("- 42");
            ret.Should().Contain("- 43");
        }

        [TestMethod]
        public void SimpleEmptyListData()
        {
            var ret = Target.Format(null, new List<int> { });
            ret.Should().Contain("data:");
        }

        [TestMethod]
        public void ComplexListData()
        {
            var ret = Target.Format(null, new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int>("bob", 42),
                new KeyValuePair<string, int>("bob2", 422)
            });
            ret.Should().Contain("key: bob");
            ret.Should().Contain("key: bob2");
            ret.Should().Contain("value: 42");
            ret.Should().Contain("value: 422");
        }

        [TestMethod]
        public void DynamicListData()
        {
            var ret = Target.Format(null, new List<object> {
                new { first = 1},
                new { junk = "bob" }
            });
            ret.Should().Contain("first: 1");
            ret.Should().Contain("junk: bob");
        }
    }
}
