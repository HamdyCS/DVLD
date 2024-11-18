using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestTypesDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static DataTable GetAllTestTypesContent()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Fees", typeof(double));

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from TestTypes";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dt.Rows.Add(reader["TestTypeID"],
                        reader["TestTypeTitle"],
                        reader["TestTypeDescription"],
                        reader["TestTypeFees"]);
                }
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

            }
            finally { connection.Close(); }


            return dt;
        }

        public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle,
           string TestTypeDescription, double TestTypeFees)

        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "update TestTypes set TestTypeTitle =  " +
                "@TestTypeTitle, TestTypeDescription = @TestTypeDescription , " +
                "TestTypeFees = @TestTypeFees " +
                "where TestTypeID = @TestTypeID ";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                result = false;
            }
            finally { connection.Close(); }


            return result;
        }

        public static bool FindTestTypeByID(int TestTypeID, ref string TestTypeTitle,
           ref string TestTypeDescription, ref double TestTypeFees)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from TestTypes where TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeTitle = Convert.ToString(reader["TestTypeTitle"]);
                    TestTypeDescription = Convert.ToString(reader["TestTypeDescription"]);
                    TestTypeFees = Convert.ToDouble(reader["TestTypeFees"]);
                    found = true;
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                found = false;
            }
            finally { connection.Close(); }



            return found;
        }
    }
}
