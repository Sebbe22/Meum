using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class KundeDatabase
    {
        public KundeDatabase()
        {
        }

        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = MeumLager; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
            k.Email = reader.GetString(3);
            k.TlfNummer = reader.GetString(4);
            k.AdresseID = reader.GetInt32(5);
            k.NewlySub = reader.GetBoolean(6);
            k.SubStart = reader.GetDateTime(7);

            return k;
        }

        public Kunde GetPersonByPhoneNo(string tlfNummer)
        {
            Kunde kunde = new Kunde();

            string queryString = "select * from Kunder where TlfNummer =  '@TlfNummer'";


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

        private Kunde AddPerson(Kunde person)
        {
            string queryString =
                "insert into Person Values(@Fornavn,@Efternavn,@Email,@TlfNummer,@AdresseID,@NewlySub)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fornavne", person.Fornavn);
                command.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@TlfNummer", person.TlfNummer);
                command.Parameters.AddWithValue("@AdresseID", person.AdresseID);
                command.Parameters.AddWithValue("@NewlySub", person.NewlySub);
                command.Parameters.AddWithValue("@SubStart", person.SubStart);


                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ArgumentException("Person er ikke oprettet");
                }

                return GetPersonByPhoneNo(person.TlfNummer);
            }



        }

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
                if (rows != 1)
                {
                    throw new ArgumentException("Person er ikke slettet");
                }

                return k;
            }
        }

        private bool UpdateKunde(string tlfNummer, Kunde kunde)
        {
            string queryString =
                "Update Person set TlfNummer = @TlfNummer, Fornavn = @Fornavn, Efternavn = @EfterNavn, Email = @Email, NewlySub = @NewlySub, SubStart = @SubStart where TlfNummer = @UpdateTlfNummer";

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
