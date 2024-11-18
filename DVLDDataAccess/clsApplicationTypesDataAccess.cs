using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsApplicationTypesDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static DataTable GetAllApplicationTypesContent()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Fees", typeof(Single));

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from ApplicationTypes";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dt.Rows.Add(reader["ApplicationTypeID"],
                        reader["ApplicationTypeTitle"],
                        reader["ApplicationFees"]);
                }
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

            }
            finally { connection.Close(); }


            return dt;
        }

        public static bool UpdateApplicationTypes(int ID, string Tilte, Single Fees)

        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "update ApplicationTypes set ApplicationTypeTitle =  @ApplicationTypeTitle, ApplicationFees = @Fees  where ApplicationTypeID = @ApplicationTypeID ";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Tilte);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ID);


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

        public static bool FindApplicationTypeByID(int ApplicationTypesID, ref string ApplicationTypesTitle,
            ref Single ApplicationFees)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from ApplicationTypes \r\nwhere ApplicationTypeID = @ApplicationTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypesID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationTypesTitle = Convert.ToString(reader["ApplicationTypeTitle"]);
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
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

        public static double GetApplicationFees(int ApplicationTypesID)
        {
            double ApplicationFees = -1;

            string Query = "select ApplicationFees from ApplicationTypes\r\nwhere ApplicationTypeID = @ApplicationTypeID ";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypesID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                //if(obj != null && int.TryParse(obj.ToString(),out int Fees))
                //{
                //    ApplicationFees = Fees;
                //}

                ApplicationFees = Convert.ToSingle(obj);



            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                ApplicationFees = -1;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationFees;
        }


    }

}
