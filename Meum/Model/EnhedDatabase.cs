using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class EnhedDatabase
    {
        public EnhedDatabase()
        {
        }

        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = MeumLager; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Enhed> GetAllEnheder()
        {
            List<Enhed> enheder = new List<Enhed>();

            string queryString = "select * from Lager";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enhed e = ReadEnhed(reader);
                    enheder.Add(e);
                }
            }


            return enheder;
        }

        public Enhed ReadEnhed(SqlDataReader reader)
        {
            Enhed e = new Enhed();

            e.VareId = reader.GetInt32(0);
            e.Navn = reader.GetString(1);
            e.Pris = reader.GetInt32(2);
            e.Antal = reader.GetInt32(3);


            return e;
        }

        public Enhed GetEnhedById(int enhedId)
        {
            Enhed enhed = new Enhed();

            string queryString = "select * from Lager where Vare_ID =  @VareId";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("VareId", enhedId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    enhed = ReadEnhed(reader);
                }
            }

            return enhed;
        }

        public Enhed AddEnhed(Enhed enhed)
        {
            string queryString =
                "insert into Lager Values(@Navn,@Pris,@Antal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Navn", enhed.Navn);
                command.Parameters.AddWithValue("@Pris", enhed.Pris);
                command.Parameters.AddWithValue("@Antal", enhed.Antal);



                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Produktet er ikke oprettet");
                }

                //return GetEnhedById(enhed.VareId);
                return enhed;
            }



        }

        public Enhed DeleteEnhedById(int vareId)
        {
            Enhed e = GetEnhedById(vareId);

            string queryString = "Delete from Lager where Vare_ID = @VareId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@VareId", vareId);

                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Enheden er ikke slettet");
                }

                return e;
            }
        }

        public bool UpdateEnhed(int vareId, Enhed enhed)
        {
            string queryString =
                "Update Enhed set VareId = @VareId, Navn = @Navn, Pris = @Pris, Antal = @Antald";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateProduktId", vareId);
                command.Parameters.AddWithValue("@Navn", enhed.Navn);
                command.Parameters.AddWithValue("@Pris", enhed.Pris);
                command.Parameters.AddWithValue("@Beskrivelse", enhed.Antal);


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
