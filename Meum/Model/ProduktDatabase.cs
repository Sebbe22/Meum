using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class ProduktDatabase
    {

        public ProduktDatabase()
        {
        }

        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MeumLager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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

        public Produkt AddProdukt(Produkt produkt)
        {
            string queryString =
                "insert into Person Values(@Navn,@Pris,@Beskrivelse)";

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
                if (rows != 1)
                {
                    throw new ArgumentException("Produkt er ikke slettet");
                }

                return p;
            }
        }

        public bool UpdateProdukt(int produktId, Produkt produkt)
        {
            string queryString =
                "Update Produkt set ProduktId = @ProduktId, Navn = @Navn, Pris = @Pris, Beskrivelse = @Beskrivelse where ProduktId = @UpdateProduktId";

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
