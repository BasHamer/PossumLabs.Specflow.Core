using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationObjectAttribute : Attribute
    {
        public ConfigurationObjectAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
    }
}
