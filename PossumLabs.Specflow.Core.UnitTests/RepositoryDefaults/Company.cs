using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.RepositoryDefaults
{
    public class Company : IValueObject
    {
    }

    public class CompanyRepository : RepositoryBase<Company>
    {
        public CompanyRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
        {

        }
    }
}
