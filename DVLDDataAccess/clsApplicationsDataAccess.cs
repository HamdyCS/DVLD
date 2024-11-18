using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsApplicationsDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static bool FindApplicationByID(int ApplicationID,
            ref int ApplicantPersonID, ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref byte ApplicationStatus,
            ref DateTime LastStatusDate, ref double PaidFees,
            ref int CreatedByUserID)
        {
            bool found = false;

            string Query = "select * from Applications\r\nwhere ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                PaidFees = Convert.ToDouble(reader["PaidFees"]);
                CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                found = true;


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

        public static bool AddNewApplication(ref int ApplicationID,
             int ApplicantPersonID, DateTime ApplicationDate,
             int ApplicationTypeID, byte ApplicationStatus,
             DateTime LastStatusDate, double PaidFees,
             int CreatedByUserID)
        {
            bool result = false;

            string Query = "INSERT INTO Applications          \r\n     VALUES\r\n           (@ApplicantPersonID\r\n           ,@ApplicationDate\r\n           ,@ApplicationTypeID\r\n           ,@ApplicationStatus\r\n           ,@LastStatusDate\r\n           ,@PaidFees\r\n           ,@CreatedByUserID); select SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int ID))
                {
                    ApplicationID = ID;
                    result = true;
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

        public static bool UpdateApplication(int ApplicationID,
             int ApplicantPersonID, DateTime ApplicationDate,
             int ApplicationTypeID, byte ApplicationStatus,
             DateTime LastStatusDate, double PaidFees,
             int CreatedByUserID)
        {
            bool result = false;

            string Query = "UPDATE Applications\r\n   SET [ApplicantPersonID] = @ApplicantPersonID\r\n      ,[ApplicationDate] = @ApplicationDate\r\n      ,[ApplicationTypeID] = @ApplicationTypeID\r\n      ,[ApplicationStatus] = @ApplicationStatus\r\n      ,[LastStatusDate] = @LastStatusDate\r\n      ,[PaidFees] = @PaidFees\r\n      ,[CreatedByUserID] = @CreatedByUserID\r\n WHERE ApplicationID = @ApplicationID\r\n";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);




            try
            {
                connection.Open();

                int RowAffected = command.ExecuteNonQuery();

                if (RowAffected > 0)
                {
                    result = true;
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

        public static bool DeleteApplication(int ApplicationID)
        {
            bool result = false;

            string Query = "delete from Applications\r\nwhere ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                int RowAffected = command.ExecuteNonQuery();

                if (RowAffected > 0)
                {
                    result = true;
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

        public static bool ChangeApplicationStatus(int ApplicationID, byte Status)
        {
            bool result = false;

            string Query = "UPDATE Applications\r\n   SET [ApplicationStatus] = @Status WHERE ApplicationID = @ApplicationID ";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@Status", Status);





            try
            {
                connection.Open();

                int RowAffected = command.ExecuteNonQuery();

                if (RowAffected > 0)
                {
                    result = true;
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

    }

}
