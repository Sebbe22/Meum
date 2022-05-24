using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class ProduktEnhederKatalog
    {
        public ProduktEnhederKatalog()
        {
        }

        private const string connectionString = @"Data Source=seba-zealand-dbserver.database.windows.net;Initial Catalog=seba-zealand-db;User ID=sebaAdmin;Password=Slange123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ProduktEnheder ReadProduktEnhed(SqlDataReader reader)
        {
            ProduktEnheder pe = new ProduktEnheder();

            pe.ProduktId = reader.GetInt32(0);
            pe.EnhedId = reader.GetInt32(1);
            pe.Antal = reader.GetInt32(2);



            return pe;
        }

        /// <summary>
        /// henter en produkt enhed fra databasen udfra ID
        /// </summary>
        /// <param name="produktId"> ID'et på det produkt som man vil have fat i </param>
        /// <returns> retunerer det produkt hvis ID matcher det givne ID</returns>
        public ProduktEnheder GetProduktEnhedById(int produktId)
        {
            ProduktEnheder pEnhed = new ProduktEnheder();

            string queryString = "select * from KompositPV where Produkt_ID =  @ProduktId";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("ProduktId", produktId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pEnhed = ReadProduktEnhed(reader);
                }
            }

            return pEnhed;
        }

        /// <summary>
        /// tilføjer en produktenhed til databasen
        /// </summary>
        /// <param name="pEnhed"> det produktenhed som skal tilføjes </param>
        /// <returns>retunerer det produktenhed som blev tilføjet </returns>
        public ProduktEnheder AddProduktEnhed(ProduktEnheder pEnhed)
        {
            string queryString =
                "insert into KompositPV Values(@Produkt_ID,@Vare_ID,@Antal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Produkt_ID", pEnhed.ProduktId);
                command.Parameters.AddWithValue("@Vare_ID", pEnhed.EnhedId);
                command.Parameters.AddWithValue("@Antal", pEnhed.Antal);



                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Produktet er ikke oprettet");
                }

                //return GetEnhedById(enhed.VareId);
                return pEnhed;
            }



        }

        /// <summary>
        /// sletter produktenhed fra databasen udfra ID
        /// </summary>
        /// <param name="produktId"> ID'et på det produkt som skal slettes </param>
        /// <returns> retunerer det produkt som blev slettet </returns>
         public Produkt DeleteProduktEnhederByProduktId(int produktId)
         { 
             ProduktKatalog _p = new ProduktKatalog();
            Produkt p = _p.GetProduktById(produktId);

            string queryString = "Delete from KompositPV where Produkt_ID = @ProduktId";

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
        public Enhed DeleteProduktEnhederByVareId(int VareId)
        {
            EnhedKatalog _e = new EnhedKatalog();
            Enhed e = _e.GetEnhedById(VareId);

            string queryString = "Delete from KompositPV where Vare_ID = @VareId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@VareId", VareId);

                int rows = command.ExecuteNonQuery();
                /*  if (rows != 1)
                  {
                      throw new ArgumentException("Produkt er ikke slettet");
                  }*/

                return e;
            }
        }

        /// <summary>
        /// updaterer antalet af en enhed på lageret
        /// </summary>
        /// <param name="enhedId"> ID'et på den enhed hvis antal skal opdateres </param>
        /// <param name="antal"> det antal der skal trækkes fra lagerets totale mængde </param>
        /// <returns> retunerer en bool som fortæller om opdateringen var en succes </returns>
        public bool UpdateAntal(int enhedId, int antal)
        {
            EnhedKatalog enhedDatabase = new EnhedKatalog();

            Enhed e = enhedDatabase.GetEnhedById(enhedId);


            int newantal = e.Antal - antal;


            string queryString =
                "Update Lager set  Antal = @Antal where Vare_ID = @UpdateVareId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UpdateVareId", enhedId);
                command.Parameters.AddWithValue("@Antal", newantal);


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
