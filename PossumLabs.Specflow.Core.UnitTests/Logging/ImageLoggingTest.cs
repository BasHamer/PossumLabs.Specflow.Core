using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.Specflow.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.Logging
{
    [TestClass]
    public class ImageLoggingTest
    {
        [TestMethod]
        public void Initialize()
        {
            var target = new ImageLogging(new PossumLabs.Specflow.Core.Configuration.ImageLoggingConfig());
        }
    }
}
