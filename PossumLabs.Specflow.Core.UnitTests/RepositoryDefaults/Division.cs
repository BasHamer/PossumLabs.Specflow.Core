using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.RepositoryDefaults
{
    public class Division : IValueObject
    {
        [DefaultToRepositoryDefault]
        public Company Company { get; set; }
    }

    public class DivisionRepository : RepositoryBase<Division>
    {
        public DivisionRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
        {

        }
    }
}
