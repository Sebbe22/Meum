using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class AdresseKatalog
    {
        public AdresseKatalog()
        {
        }

        private const string connectionString =
            @"Data Source=seba-zealand-dbserver.database.windows.net;Initial Catalog=seba-zealand-db;User ID=sebaAdmin;Password=Slange123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        ///<summary>
        /// denne metode henter alle adresser fra adresse databasen og putter dem i en liste
        /// </summary>
        /// <returns> retunerer en liste med alle adresser fra databasen </returns>
     
        public List<Adresse> GetAllAdresses()
        {
            List<Adresse> adresses = new List<Adresse>();

            string queryString = "select * from Adresse";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Adresse a = ReadAdresse(reader);
                    adresses.Add(a);
                }
            }


            return adresses;
        }



        private Adresse ReadAdresse(SqlDataReader reader)
        {
            Adresse a = new Adresse();


            a.Vejnavn = reader.GetString(1);
            a.HusNr = reader.GetString(2);
            a.Etage = reader.GetString(3);
            a.ById = reader.GetInt32(4);
            a.AdresseId = reader.GetInt32(0);

            return a;
        }

        /// <summary>
        /// finder alle adresser hvor vejnavnet matcher input
        /// </summary>
        /// <param name="vejnavn"> det vejnavn man vil søge på skal være en string </param>
        /// <returns> retunerer alle Adresser hvor vejnavnet matcher input </returns>
        public Adresse GetAdresseByVejnav(string vejnavn)
        {
            Adresse adresse = new Adresse();

            string queryString = "select * from Adresse where Vejnavn = @Vejnavn";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Vejnavn", vejnavn);

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                adresse = ReadAdresse(reader);
                return adresse;

            }
        }

        /// <summary>
        /// tilføjer en addresse til databasen
        /// </summary>
        /// <param name="adresse"> den adresse man vil tilføje til databasen </param>
        /// <returns> retunerer den adresse som blev tilføjet </returns>
        public Adresse AddAdresse(Adresse adresse)
        {
            string queryString =
                "insert into Adresse Values(@Vejnavn,@HusNr,@Etage,@By_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Vejnavn", adresse.Vejnavn);
                command.Parameters.AddWithValue("@HusNr", adresse.HusNr);
                command.Parameters.AddWithValue("@Etage", adresse.Etage);
                command.Parameters.AddWithValue("@By_ID", adresse.ById);



                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Adressen er ikke oprettet");
                }

                return adresse;
            }



        }


        /// <summary>
        /// Opdaterer en existerende adresse
        /// </summary>
        /// <param name="vejnavn"> vejnavnet på den adresse som skal opdateres </param>
        /// <param name="adresse"> den Adresse som indholder den opdaterede information </param>
        /// <returns>retunerer en bool som siger hvorvidt opdateringen er godkendt </returns>
        public bool UpdateAdresse(string vejnavn, Adresse adresse)
        {
            string queryString =
                "Update Adresse set Vejnavn = @Vejnavn, HusNr = @Husnr, Etage = @Etage";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Adresse", adresse);
                command.Parameters.AddWithValue("@Vejnavn", adresse.Vejnavn);
                command.Parameters.AddWithValue("@HusNr", adresse.HusNr);
                command.Parameters.AddWithValue("@Etage", adresse.Etage);


                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
