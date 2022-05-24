using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Meum.Model
{
    public class SalgKatalog
    {
        private const string connectionString = @"Data Source=seba-zealand-dbserver.database.windows.net;Initial Catalog=seba-zealand-db;User ID=sebaAdmin;Password=Slange123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// henter alle salg fra databasen og putter dem i en liste
        /// </summary>
        /// <returns> retunerer en liste med alle salg </returns>
        public List<Salg> GetAllSalg()
        {
            List<Salg> salg = new List<Salg>();

            string queryString = "select * from Salg";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Salg s = ReadSalg(reader);
                    salg.Add(s);
                }
            }


            return salg;
        }

        /// <summary>
        /// filtrerer listen af salg så man får en ny liste med kun de salg man vil have udfra salg ID
        /// </summary>
        /// <param name="salg"> listen af salg som skal filtræres </param>
        /// <param name="id"> det ID man vil filtrære og have med i den nye liste </param>
        /// <returns> retunerer en ny liste af salg hvor kun salg med samme ID som indtastet er med </returns>
        public List<Salg> FilterById(List<Salg> salg, int id)
        {
            List<Salg> salgById = new List<Salg>();

            foreach (Salg s in salg)
            {
                if (s.ProduktId == id)
                {
                    salgById.Add(s);
                }
            }

            return salgById;
        }
        private Salg ReadSalg(SqlDataReader reader)
        {
            Salg s = new Salg();
            s.Id = reader.GetInt32(0);
            s.ProduktId = reader.GetInt32(1);
            s.Dato = reader.GetDateTime(2);
            s.Antal = reader.GetInt32(3);

            return s;
        }

        /// <summary>
        /// tjekker hvor mange skraber der er blevet solgt iløbet af det sidste år 
        /// </summary>
        /// <param name="salgsListeSkraber"> liste af solgte skraberer </param>
        /// <returns> retunerer hvor mange skraberer der er solgt i det sidste år </returns>
        public double skraberSolgt(List<Salg> salgsListeSkraber)
        {
            double solgteVarer = 0;
            foreach (Salg s in salgsListeSkraber)
            {
                //Tager alle månederne fra den her måned og frem sidste år.
                if (s.Dato.Month >= DateTime.Now.Month && s.Dato.Year == DateTime.Now.Year - 1)
                {
                    solgteVarer = solgteVarer + s.Antal;

                }
                //Tager alle månederne fra januar og frem til den nuværende måned
                if (s.Dato.Month <= DateTime.Now.Month - 1 && s.Dato.Year == DateTime.Now.Year)
                {
                    solgteVarer = solgteVarer + s.Antal;

                }


            }
            return solgteVarer;

        }

        /// <summary>
        /// Beregner hvor mange nyeAbonnenter der baseret på tidligere data forventes at komme per måned
        /// </summary>
        /// <param name="KundeListe"> liste af kunder der skal tjekkes for nye abonnementer </param>
        /// <returns> retunerer antal nye abonnementer fra det sidste år </returns>
        public double NyeAbonnenter(List<Kunde> KundeListe)
        {

            double nyeAbonnenter = 0;
            foreach (Kunde k in KundeListe)
            {
                //Tæller hvor mange der har meldt sig til i perioden fra samme måned sidste år og frem til December det år
                if (k.SubStart.Month >= DateTime.Now.Month && k.SubStart.Year == DateTime.Now.Year - 1)
                {
                    nyeAbonnenter++;


                }
                //Tæller hvor mange der har meldt sig til i perioden fra januar dette år og frem til sidste måned.
                if (k.SubStart.Month <= DateTime.Now.Month - 1 && k.SubStart.Year == DateTime.Now.Year)
                {
                    nyeAbonnenter++;


                }


            }

            return nyeAbonnenter;

        }

        /// <summary>
        /// Beregner den ønsket mindsteværdi for antallet af håndtag i lageret.
        /// </summary>
        /// <param name="abonnenter"> antal aktive abonnementer </param>
        /// <returns> retunerer mindsteværdien for antal håndtag i lageret </returns>
        public double mindsteVaerdiHåndtag(double abonnenter)
        {
            double mindsteværdi = Math.Ceiling(abonnenter / 12);
            return mindsteværdi;
        }


        /// <summary>
        /// Beregner den ønsket mindsteværdi for antallet af skrabere i lageret.
        /// </summary>
        /// <param name="abonnenter"> antal aktive abonnementer </param>
        /// <param name="solgtSkraber"> antal solgte skraberer iløbet af det sidste år </param>
        /// <returns> retunerer mindsteværdi for antal skraberer på lager </returns>
        public double mindsteSkraber(double abonnenter, double solgtSkraber)
        {

            double mindsteværdi = Math.Ceiling((abonnenter / 12) * 2 + (solgtSkraber / 12) * 4);
            return mindsteværdi;
        }

        /// <summary>
        /// Beregner hvor tæt lagerbeholdningen er på den ønsket mindsteværdi.Hvor y 
        /// er lagerbeholdning og x er mindsteværdi for hvad lagerbeholdningen må være.
        /// </summary>
        /// <param name="lagerbeholdning"> den nuværende lagerbeholdning </param>
        /// <param name="mindsteværdi"> mindsteværdien for lagerbeholdning </param>
        /// <returns></returns>
        public string LagerStatus(double lagerbeholdning, double mindsteværdi)
        {
            if (lagerbeholdning < mindsteværdi * 2)
            {
                return "rød";
            }

            if (lagerbeholdning < mindsteværdi * 4)
            {
                return "gul";
            }

            if (lagerbeholdning >= mindsteværdi * 4)
            {
                return "grøn";
            }

            return "fejl";
        }

        /// <summary>
        /// laver et array med antal salg for hver måned i det forgangne år 
        /// </summary>
        /// <param name="Salg"> listen af salg </param>
        /// <returns> retunerer et array med antal salg for hver måned </returns>
        public int[] PlotData(List<Salg> Salg)
        {
            List<int> salg = new List<int>();
            int[] array;
            foreach (Salg s in Salg)
            {
                //Tager alle månederne fra den her måned og frem sidste år.
                if (s.Dato.Month >= DateTime.Now.Month && s.Dato.Year == DateTime.Now.Year - 1)
                {
                    salg.Add(s.Antal);

                }
                //Tager alle månederne fra januar og frem til den nuværende måned
                if (s.Dato.Month <= DateTime.Now.Month - 1 && s.Dato.Year == DateTime.Now.Year)
                {
                    salg.Add(s.Antal);

                }


            }

            array = salg.ToArray();



            return array;
        }

        /// <summary>
        /// Metode som sætter månederne ind i array som kan bruges til x-aksen for grafer
        /// </summary>
        /// <returns> retunerer et array med måneder fra dags måned og et år tilbage </returns>
        public string[] PlotDataXakse()
        {
            int t = 12;
            List<string> måneder = new List<string>();
            string[] xAkse;
            while (t > 0)
            {
                måneder.Add(DateTime.Now.AddMonths(-t).ToString("MMM yyyy"));
                t = t - 1;
            }

            xAkse = måneder.ToArray();
            return xAkse;
        }

        /// <summary>
        /// Tilføjer salg til Database
        /// </summary>
        /// <param name="salg"> det salg der skal tilføjes til databasen </param>
        public void AddSalgEntry(Salg salg)
        {
            string queryString =
                "insert into Salg Values(@ProduktId,@Dato,@Antal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@ProduktId", salg.ProduktId);
                command.Parameters.AddWithValue("@Dato", salg.Dato);
                command.Parameters.AddWithValue("@Antal", salg.Antal);


                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("SalgEntry er ikke oprettet");
                }

                
            }



        }

        /// <summary>
        /// opdaterer antal på et salg
        /// </summary>
        /// <param name="SalgsId"> ID'et på det salg som skal opdateres </param>
        /// <param name="antal"> antallet der skal opdateres til </param>
        /// <returns></returns>
        public bool UpdateSalgAntal(int SalgsId, int antal)
        {
            string queryString =
                "Update Salg set  Antal = @Antal where SalgId = @UpdateSalgId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateSalgId", SalgsId);
                command.Parameters.AddWithValue("@Antal", antal);
               

                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Metode som gemmer salg i salgsdatabaen og opretter en ny måned hvis den mangler. 
        /// </summary>
        /// <param name="newlySub"> skal være true hvis det er kundens første forsendelse </param>
        public void SalgPerMåned(bool newlySub)
        {
            List<Salg> salgListe = new List<Salg>();
            salgListe = GetAllSalg();
            bool entryExists = false;
            int salgsId = 0;
            int gammeltAntal=0;
            if (newlySub == true)
            {
                foreach (Salg s in salgListe)
                {
                    if (s.ProduktId == 2 && s.Dato.Month == DateTime.Now.Month && s.Dato.Year == DateTime.Now.Month)
                    {
                        entryExists = true;
                        salgsId = s.Id;
                        gammeltAntal = s.Antal;


                    }
                    
                }

                if (entryExists == true)
                {
                    UpdateSalgAntal(salgsId,gammeltAntal + 1);
                }

                if (entryExists == false)
                {
                    Salg salg = new Salg(1, 2, DateTime.Now, 1);
                    AddSalgEntry(salg);
                }
            }

            if (newlySub == false)
            {
                foreach (Salg s in salgListe)
                {
                    if (s.ProduktId == 3 && s.Dato.Month == DateTime.Now.Month && s.Dato.Year == DateTime.Now.Year)
                    {
                        entryExists = true;
                        salgsId = s.Id;
                        gammeltAntal = s.Antal;


                    }

                }

                if (entryExists == true)
                {
                    UpdateSalgAntal(salgsId, gammeltAntal + 1);
                }

                if (entryExists == false)
                {
                    Salg salg = new Salg(1, 3, DateTime.Now, 1);
                    AddSalgEntry(salg);
                }
            }
        }


    }
}

