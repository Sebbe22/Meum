using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class ProduktKatalog
    {

        public ProduktKatalog()
        {
        }

        private const string connectionString = @"Data Source=seba-zealand-dbserver.database.windows.net;Initial Catalog=seba-zealand-db;User ID=sebaAdmin;Password=Slange123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// henter alle produkter fra databasen og putter dem i en liste
        /// </summary>
        /// <returns> retunerer listen med alle produkter </returns>
        public List<Produkt> GetAllProdukter()
        {
            List<Produkt> produkter = new List<Produkt>();

            string queryString = "select * from Produkt";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Produkt p = ReadProdukt(reader);
                    produkter.Add(p);
                }
            }


            return produkter;
        }

        public Produkt ReadProdukt(SqlDataReader reader)
        {
            Produkt p = new Produkt();

            p.ProduktId = reader.GetInt32(0);
            p.Navn = reader.GetString(1);
            p.Pris = reader.GetInt32(2);
            p.Beskrivelse = reader.GetString(3);


            return p;
        }

        /// <summary>
        /// henter et produkt ud fra det givet ID
        /// </summary>
        /// <param name="produktId"> ID'et på det produkt man vil have fat i </param>
        /// <returns> retunerer det produkt hvis ID matcher den givne int </returns>
        public Produkt GetProduktById(int produktId)
        {
            Produkt produkt = new Produkt();

            string queryString = "select * from Produkt where Produkt_ID =  @ProduktId";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("ProduktId", produktId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    produkt = ReadProdukt(reader);
                }
            }

            return produkt;
        }

        /// <summary>
        /// henter et produkt ud fra det navn man vil finde
        /// </summary>
        /// <param name="Navn"> navnet på det produkt man vil have fat i </param>
        /// <returns> retunerer det produkt hvor navnet matcher det input man gav </returns>
        public Produkt GetProduktByNavn(string Navn)
        {
            Produkt produkt = new Produkt();



            string queryString = "select * from Produkt where Navn = @Navn";




            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Navn", Navn);



                SqlDataReader reader = command.ExecuteReader();



                reader.Read();



                produkt = ReadProdukt(reader);
                return produkt;



            }
        }

        /// <summary>
        /// tilføjer et produkt til databasen
        /// </summary>
        /// <param name="produkt"> det produkt man vil tilføje til databasen </param>
        /// <returns> retunerer det produkt som blev tilføjet </returns>
        public Produkt AddProdukt(Produkt produkt)
        {
            string queryString =
                "insert into Produkt Values(@Navn,@Pris,@Beskrivelse)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Navn", produkt.Navn);
                command.Parameters.AddWithValue("@Pris", produkt.Pris);
                command.Parameters.AddWithValue("@Beskrivelse", produkt.Beskrivelse);



                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Produktet er ikke oprettet");
                }

                return GetProduktById(produkt.ProduktId);
            }



        }

        /// <summary>
        /// sletter et produkt fra databasen udfra ID
        /// </summary>
        /// <param name="produktId"> ID'et på det produkt som skal slettes </param>
        /// <returns> retunerer det produkt som blev slettet </returns>
        public Produkt DeleteProduktById(int produktId)
        {
            Produkt p = GetProduktById(produktId);

            string queryString = "Delete from Produkt where Produkt_ID = @ProduktId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@ProduktId", produktId);
                
                int rows = command.ExecuteNonQuery();
              /*  if (rows != 1)
                {
                    throw new ArgumentException("Produkt er ikke slettet");
                }*/

                return p;
            }
        }

        /// <summary>
        /// opdaterer et produkt i databasen
        /// </summary>
        /// <param name="produktId"> ID'et på det produkt som skal opdateres </param>
        /// <param name="produkt"> det produkt som der bliver opdateret til </param>
        /// <returns> retunerer en bool som siger om det er blevet opdateret </returns>
        public bool UpdateProdukt(int produktId, Produkt produkt)
        {
            string queryString =
                "Update Produkt set Navn = @Navn, Pris = @Pris, Beskrivelse = @Beskrivelse where Produkt_ID = @UpdateProduktId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateProduktId", produktId);
                command.Parameters.AddWithValue("@Navn", produkt.Navn);
                command.Parameters.AddWithValue("@Pris", produkt.Pris);
                command.Parameters.AddWithValue("@Beskrivelse", produkt.Beskrivelse);

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
