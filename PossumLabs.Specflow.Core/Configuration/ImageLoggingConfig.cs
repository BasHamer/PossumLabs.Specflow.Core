using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.Configuration
{
    [ConfigurationObject("ImageLogging")]
    public class ImageLoggingConfig
    {
        public ImageLoggingConfig()
        {
            SizePercentage = .05;
            Color = "DarkOrange";
        }

        [ConfigurationMember("SizePercentage")]
        public double SizePercentage { get; set; }

        [ConfigurationMember("Color")]
        public string Color { get; set; }
    }
}
