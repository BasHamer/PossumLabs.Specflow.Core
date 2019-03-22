using System.Collections.Generic;
using System.Reflection;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    public class LinkMethod
    {
        public List<LinkMethodParameter> Parameters { get; set; }
        public string JsonAttribute { get; set; }
        public MethodInfo Method { get; set; }
    }
}
