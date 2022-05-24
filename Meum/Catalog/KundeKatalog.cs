using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class KundeKatalog : IKundeKatalog
    {
        public KundeKatalog()
        {
        }

        private const string connectionString = @"Data Source=seba-zealand-dbserver.database.windows.net;Initial Catalog=seba-zealand-db;User ID=sebaAdmin;Password=Slange123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// tæller mængden af abonnementer
        /// </summary>
        /// <param name="kunde"> listen af aktive kunder som har et abonnement </param>
        /// <returns> retunerer en int som er det totale antal abonnementer</returns>
        public int SubCountKunde(List<Kunde> kunde)
        {
            int subCount = 0;

            foreach (Kunde k in kunde)
            {
                subCount = subCount + 1;
            }

            return subCount;
        }

        /// <summary>
        /// henter alle kunder fra databasen og putter dem i en liste
        /// </summary>
        /// <returns> retunerer en liste med alle kunder </returns>
        public List<Kunde> GetAllKunder()
        {
            List<Kunde> kunder = new List<Kunde>();

            string queryString = "select * from Kunder";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Kunde k = ReadKunde(reader);
                    kunder.Add(k);
                }
            }


            return kunder;
        }

        private Kunde ReadKunde(SqlDataReader reader)
        {
            Kunde k = new Kunde();

            k.IdKunde = reader.GetInt32(0);
            k.Fornavn = reader.GetString(1);
            k.Efternavn = reader.GetString(2);
            k.AbonType = reader.GetInt32(3);
            k.Email = reader.GetString(4);
            k.TlfNummer = reader.GetString(5);
            k.AdresseID = reader.GetInt32(6);
            k.NewlySub = reader.GetBoolean(7);
            k.SubStart = reader.GetDateTime(8);
            k.Afsend = reader.GetBoolean(9);

            return k;
        }

        /// <summary>
        /// får fat i en kunde fra databasen udfra det tlf nummer man har givet
        /// </summary>
        /// <param name="tlfNummer"> det telefon nummer man vil finde en kunde på </param>
        /// <returns> retunerer den kunde hvis tlf nummer matcher det man indtastede</returns>
        public Kunde GetPersonByPhoneNo(string tlfNummer)
        {
            Kunde kunde = new Kunde();

            string queryString = "select * from Kunder where TlfNummer = @TlfNummer";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@TlfNummer", tlfNummer);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    kunde = ReadKunde(reader);
                    return kunde;
                }
            }

            return kunde;
        }

        /// <summary>
        /// tilføjer en kunde til databasen
        /// </summary>
        /// <param name="person"> den kunde man vil tilføje til databasen </param>
        /// <returns> retunerer den kunde som blev tilføjet </returns>
        public Kunde AddPerson(Kunde person)
        {
            string queryString =
                "insert into Kunder Values(@Fornavn,@Efternavn,@AbonType,@Email,@TlfNummer,@AdresseID,@NewlySub, @SubStart, @Afsend)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                command.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                command.Parameters.AddWithValue("@AbonType", person.AbonType);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@TlfNummer", person.TlfNummer);
                command.Parameters.AddWithValue("@AdresseID", person.AdresseID);
                command.Parameters.AddWithValue("@NewlySub", person.NewlySub);
                command.Parameters.AddWithValue("@SubStart", person.SubStart);
                command.Parameters.AddWithValue("@Afsend", person.Afsend);


                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Person er ikke oprettet");
                }

                return GetPersonByPhoneNo(person.TlfNummer);
            }



        }

        /// <summary>
        /// sletter en kunde fra databasen udfra telefon nummer
        /// </summary>
        /// <param name="TlfNummer"> nummeret på den kunde som skal slettes </param>
        /// <returns> retunerer den kunde som blev slettet </returns>
        public Kunde DeletePersonByPhoneNo(string TlfNummer)
        {
            Kunde k = GetPersonByPhoneNo(TlfNummer);

            string queryString = "Delete from Kunder where TlfNummer = @TlfNummer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@TlfNummer", TlfNummer);

                int rows = command.ExecuteNonQuery();
                /*if (rows != 1)
                {
                    throw new ArgumentException("Person er ikke slettet");
                }*/

                return k;
            }
        }

        /// <summary>
        /// opdaterer en kunde i databasen
        /// </summary>
        /// <param name="tlfNummer"> tlf nummeret på den kunde som skal opdateres </param>
        /// <param name="kunde"> den kunde man vil opdaterer til </param>
        /// <returns></returns>
        public bool UpdateKunde(string tlfNummer, Kunde kunde)
        {
            string queryString =
                "Update Kunder set TlfNummer = @TlfNummer, Fornavn = @Fornavn, Efternavn = @EfterNavn, Email = @Email, NewlySub = @NewlySub, SubStart = @SubStart, Afsend = @Afsend where TlfNummer = @UpdateTlfNummer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateTlfNummer", tlfNummer);
                command.Parameters.AddWithValue("@TlfNummer", kunde.TlfNummer);
                command.Parameters.AddWithValue("@Fornavn", kunde.Fornavn);
                command.Parameters.AddWithValue("@Efternavn", kunde.Efternavn);
                command.Parameters.AddWithValue("@Email", kunde.Email);
                command.Parameters.AddWithValue("@NewlySub", kunde.NewlySub);
                command.Parameters.AddWithValue("@SubStart", kunde.SubStart);
                command.Parameters.AddWithValue("@Afsend", kunde.Afsend);

                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    return false;
                }

                return true;
            }

        }

        /// <summary>
        /// tjekker hvilke forsendelser der går ud på den gældende dag
        /// </summary>
        /// <param name="x"> x er antal dage frem i tiden man vil se eksempeltvis hvis jeg vil se 
        /// leveringer der skal sendes på dagen om 3 dage skal x være 3</param>
        /// <param name="kundeListe"> listen af kunder som skal tjekkes for forsendelser </param>
        /// <returns> retunerer en liste af kunder som skal have forsendelse på den givne dag </returns>
        public List<Kunde> Forsendelser(double x, List<Kunde> kundeListe)
        {

            //Meum sender samme dag hvis man bestiller inden klokken 14. Derfor bestemmes dagens forsendelser ud fra perioden mellem klokken 14-24 dagen før og 24-14 på selve dagen.
            List<Kunde> temporaryForsendelsesListeKundecsList = new List<Kunde>();
            List<Kunde> forsendelser = new List<Kunde>();
            foreach (Kunde k in kundeListe)
            {
                DateTime y = DateTime.Now;
                y = y.AddDays(x);
                if (k.SubStart.Day == y.Day - 1 && k.SubStart.Hour > 13)
                {
                    temporaryForsendelsesListeKundecsList.Add(k);
                }

                if (k.SubStart.Day == y.Day && k.SubStart.Hour <= 13)
                {
                    temporaryForsendelsesListeKundecsList.Add(k);
                }
            }
            foreach (Kunde k in temporaryForsendelsesListeKundecsList)
            {

                if (k.AbonType == 1)
                {
                    forsendelser.Add(k);
                }

                if (k.AbonType == 2)
                {

                    if (DateTime.Now.Month == k.SubStart.Month || DateTime.Now.Month == Måneder(k.SubStart.Month, 2) || DateTime.Now.Month == Måneder(k.SubStart.Month, 4) || DateTime.Now.Month == Måneder(k.SubStart.Month, 6) || DateTime.Now.Month == Måneder(k.SubStart.Month, 8) || DateTime.Now.Month == Måneder(k.SubStart.Month, 10))
                    {
                        forsendelser.Add(k);
                    }

                }

                if (k.AbonType == 3)
                {
                    if (DateTime.Now.Month == k.SubStart.Month || DateTime.Now.Month == Måneder(k.SubStart.Month, 3) ||
                        DateTime.Now.Month == Måneder(k.SubStart.Month, 6) || DateTime.Now.Month == Måneder(k.SubStart.Month, 9))
                    {
                        forsendelser.Add(k);
                    }
                }
                
            }

            return forsendelser;
        }

        /// <summary>
        /// sørger for at måneder går til næste år istedet for at gå over 12
        /// </summary>
        /// <param name="y"> y er den måned vi er i nu </param>
        /// <param name="x"> x er det antal måneder der bliver lagt til</param>
        /// <returns> retunerer en int som representerer den måned vi er i efter x er lagt til y </returns>
        public int Måneder(int y, int x)
        {
            int d1 = 0;


            if (y + x > 12)
            {
                d1 = y + x - 12;
            }
            else
            {
                d1 = y + x;
            }


            return d1;
        }

        /// <summary>
        /// sætter adresse ID'et på en kunde
        /// </summary>
        /// <param name="Id"> det ID som adresse ID'et får </param>
        /// <param name="kunde"> den kunde hvor adresse ID'et skal ændres på </param>
        public void SetAdresseID(int Id, Kunde kunde)
        {
            kunde.AdresseID = Id;
        }

        /// <summary>
        /// beregner hvornår lageret af skrabehoveder bliver tomt udra det nuværende antal kunder
        /// </summary>
        /// <param name="antalLager"> antal skrabehovder på lager </param>
        /// <param name="kundelist"> listen af aktive kunder </param>
        /// <returns> retunerer dato for hvornår skrabehovder løber tør </returns>
        public DateTime LagerTomSkrabehovede(int antalLager, List<Kunde> kundelist)
        {
            int countDage = 0;
            int countForbrug = 0;

            List<Kunde> TotalKunde = new List<Kunde>();

            while (antalLager > countForbrug)
            {
                TotalKunde = Forsendelser(countDage, kundelist);
                countDage++;
                foreach(Kunde k in TotalKunde)
                {
                    if (k.NewlySub == false)
                    {
                        countForbrug = countForbrug + 4;
                    }
                    if (k.NewlySub == true)
                    {
                        countForbrug = countForbrug + 2;
                        k.NewlySub = false;
                    }
                }
            }

            return DateTime.Now.AddDays(countDage);
        }

        /// <summary>
        /// opdaterer en kundes status på boolen "newlySub" 
        /// </summary>
        /// <param name="tlfNummer"> nummeret på den kunde der skal opdateres på </param>
        /// <param name="sub"> hvad boolen "newlySub" skal opdateres til </param>
        /// <returns> retunerer hvad boolen er opdateret til </returns>
        public bool UpdateNewlySub(string tlfNummer, Boolean sub)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString =
                    "Update Kunder set  NewlySub = @NewlySub where TlfNummer = @UpdateTlfNummer";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateTlfNummer", tlfNummer);
                command.Parameters.AddWithValue("@NewlySub", sub);

                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    return false;
                }

                return false;
            }
        }

        /// <summary>
        /// sætter boolen Afsend tilbage til false 2 dage efter en kundes forsendelse
        /// så den vil få sendt en pakke igen næste gang den skal have en forsendelse
        /// </summary>
        /// <param name="kundeliste"> listen af alle kunder </param>
        public void ReturnToFalse(List<Kunde> kundeliste)
        {
            foreach (Kunde k in kundeliste)
            {
                if (k.Afsend == true && k.SubStart.AddDays(2).Day == DateTime.Now.Day)
                {
                    k.Afsend = false;
                    UpdateKunde(k.TlfNummer, k);
                }
            }
        }

        Kunde IKundeKatalog.ReadKunde(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
