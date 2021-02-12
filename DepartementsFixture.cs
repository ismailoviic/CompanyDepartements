using CompanyDepartements;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepartementsTests
{
    public class DepartementsFixture : IDisposable
    {
        public IDepartements Departement { get; private set; }
        public DepartementsFixture()
        {
            Departement = new Departements();
        }
        public void Dispose()
        {

        }
    }
}
