using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class EnhedKatalog
    {
        public EnhedKatalog()
        {
        }

        private const string connectionString = @"Data Source=seba-zealand-dbserver.database.windows.net;Initial Catalog=seba-zealand-db;User ID=sebaAdmin;Password=Slange123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        /// <summary>
        /// henter alle enheder fra lager databasen og putter dem i en liste
        /// </summary>
        /// <returns> retunerer en liste med alle enheder fra lager databasen </returns>
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

        /// <summary>
        /// henter en enhed fra databasen udfra ID'et
        /// </summary>
        /// <param name="enhedId"> ID'et på den enhed man gerne vil have fat i skal være en int </param>
        /// <returns> retunerer en enhed hvis ID matcher det input man gav </returns>
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

        /// <summary>
        /// tilføjer en enhed til databasen
        /// </summary>
        /// <param name="enhed"> den enhed som bliver tilføjet til databasen </param>
        /// <returns> retunerer den enhed som blev tilføjet </returns>
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


                return enhed;
            }



        }

        /// <summary>
        /// sletter en enhed fra Lager databasen udfra det ID som er givet
        /// </summary>
        /// <param name="vareId"> ID'et på den enhed som skal slettes </param>
        /// <returns> retunerer den enhed som blev slettet </returns>
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

        /// <summary>
        /// får fat i en enhed i databasen udfra navnet
        /// </summary>
        /// <param name="name"> navnet på den enhed som man vil have fat på </param>
        /// <returns> retunerer den enhed som matcher det navn der blev givet </returns>
        public Enhed GetEnhedByName(string name)
        {
            Enhed enhed = new Enhed();
            List<Enhed> enheder = GetAllEnheder();
            foreach (Enhed e in enheder)
            {
                string n = e.Navn.Trim();
                if (n == name)
                {
                    enhed = e;
                }
            }

            return enhed;
        }

        /// <summary>
        /// Opdaterer værdier på en enhed
        /// </summary>
        /// <param name="vareId"> ID'et på den enhed som skal opdateres </param>
        /// <param name="enhed"> den enhed man vil opdaterer til </param>
        /// <returns> retunerer en bool som fortæller om enheden blev opdateret eller ej </returns>
        public bool UpdateEnhed(int vareId, Enhed enhed)
        {
            string queryString =
                "Update Lager set Navn = @Navn, Pris = @Pris, Antal = @Antal where Vare_ID = @UpdateProduktId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateProduktId", vareId);
                //command.Parameters.AddWithValue("@VareId", vareId);
                command.Parameters.AddWithValue("@Navn", enhed.Navn);
                command.Parameters.AddWithValue("@Pris", enhed.Pris);
                command.Parameters.AddWithValue("@Antal", enhed.Antal);


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
