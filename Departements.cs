using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyDepartements
{
    public class Departements : IDepartements
    {
        public void Affect(List<Recru> departements, List<Recru> recrus) => departements.AddRange(recrus);
        public void Affect(List<Recru> departements, Recru recru) => departements.Add(recru);
        public void DeleteRecrus(List<Recru> departements, List<Recru> recrus) => recrus.ForEach(recru => DeleteRecrus(departements, recru));
        public void DeleteRecrus(List<Recru> departements, Recru recru) => departements.RemoveAll(rec => rec.Name == recru.Name && rec.Email == recru.Email);


        public Recru GetRecruByName(List<Recru> departements, string name) => departements.First(rec => rec.Name == name);
        public List<Recru> GetRecrusInDepartement(List<Recru> departements, DepartementsType departement) => departements.Where(recru => recru.Departements == departement).ToList();
        public Recru GetRecruByEmail(List<Recru> departements, string email) => departements.Single(rec => rec.Email == email);
        public bool DepartementContainsRecru(List<Recru> departements, string name) => departements.Any(recru => recru.Name == name);



        public List<Recru> Search(List<Recru> departements, string searchTerm)
        {

            List<Recru> result = new List<Recru>();
            if (string.IsNullOrEmpty(searchTerm)) return result;
            if (searchTerm.Contains('@')) { return departements.Where(recru => recru.Email.Contains(searchTerm)).ToList(); }
            var departementString = new[] {
                new {str= "Dev", dep = DepartementsType.Dev },
                new {str= "Marketing",dep=DepartementsType.Marketing } ,
                new {str= "Sales",dep=DepartementsType.Sales } ,
                new {str= "Rh",dep=DepartementsType.Rh }
            };
            foreach (var departement in departementString)
            {
                if (searchTerm.Equals(departement.str, StringComparison.InvariantCultureIgnoreCase))
                { return departements.Where(recru => recru.Departements == departement.dep).ToList(); }
            }

            return departements.Where(recru => recru.Name.Contains(searchTerm)).ToList();
        }
    }
}
