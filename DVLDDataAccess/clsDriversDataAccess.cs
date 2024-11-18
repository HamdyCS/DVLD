using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public class clsDriversDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from MyDriverView;";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

            }
            finally
            {
                connection.Close();
            }


            return dt;
        }

        public static bool CheckIfThisPersonIsDriver(int PersonID)
        {
            bool result = true;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select PersonID from Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj == null)
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                result = true;
            }
            finally
            {
                connection.Close();

            }

            return result;
        }

        public static bool AddNewDriver(ref int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "insert into Drivers\r\n values\r\n (@PersonID,@CreatedByUserID,@CreatedDate) select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
                {
                    DriverID = Convert.ToInt32(obj);
                    return true;
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                result = false;
            }
            finally
            {
                connection.Close();

            }

            return result;
        }

        public static bool FindDriver(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool found = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from Drivers where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    found = true;
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                found = false;
            }
            finally
            {
                connection.Close();

            }

            return found;
        }

        public static bool FindDriverByPersonID(ref int DriverID, int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    found = true;
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                found = false;
            }
            finally
            {
                connection.Close();

            }

            return found;
        }
    }
}
