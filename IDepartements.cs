using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDepartements
{
    public interface IDepartements
    {
        void Affect(List<Recru> departements, List<Recru> recrus);
        void Affect(List<Recru> departements, Recru recru);
        void DeleteRecrus(List<Recru> departements, List<Recru> recrus);
        void DeleteRecrus(List<Recru> departements, Recru recru);
        List<Recru> Search(List<Recru> departements, string searchTerm);


        bool DepartementContainsRecru(List<Recru> departements, string name);
        Recru GetRecruByName(List<Recru> departements, string name);
        Recru GetRecruByEmail(List<Recru> departements, string email);
        List<Recru> GetRecrusInDepartement(List<Recru> departements, DepartementsType departement);
    }
}
