using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDepartements
{
    public class Recru
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Email { get; private set; }
        public Position Position { get; private set; }
        public DepartementsType Departements { get; private set; }
        public Recru(string name, int age, string email, Position position, DepartementsType departements)
        {
            Name = name;
            Age = age;
            Email = email;
            Position = position;
            Departements = departements;
        }
    }
}
