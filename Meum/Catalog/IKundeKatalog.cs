using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public interface IKundeKatalog
    {
        public int SubCountKunde(List<Kunde> kunde);

        public List<Kunde> GetAllKunder();

        Kunde ReadKunde(SqlDataReader reader);

        public Kunde GetPersonByPhoneNo(string tlfNummer);

        public Kunde AddPerson(Kunde person);

        public Kunde DeletePersonByPhoneNo(string TlfNummer);

        public bool UpdateKunde(string tlfNummer, Kunde kunde);

        public List<Kunde> Forsendelser(double x, List<Kunde> kundeListe);

        public int Måneder(int y, int x);

        public void SetAdresseID(int Id, Kunde kunde);

        public DateTime LagerTomSkrabehovede(int antalLager, List<Kunde> kundelist);

        public bool UpdateNewlySub(string tlfNummer, Boolean sub);

        public void ReturnToFalse(List<Kunde> kundeliste);
    }
}
