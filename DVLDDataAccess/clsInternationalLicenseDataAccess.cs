using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsInternationalLicenseDataAccess
    {

        public static DataTable GetAllInternationalPersonLicense(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "SELECT   [I.D.L.ID] =  InternationalLicenses.InternationalLicenseID,\r\n[Application ID] = InternationalLicenses.ApplicationID,\r\n[L.License.ID] = InternationalLicenses.IssuedUsingLocalLicenseID,\r\n[Issue Date] = InternationalLicenses.IssueDate,\r\n[Expiration Date] = InternationalLicenses.ExpirationDate,\r\n[Is Active] = InternationalLicenses.IsActive     \r\nFROM            InternationalLicenses INNER JOIN\r\n                         Applications ON InternationalLicenses.ApplicationID = Applications.ApplicationID INNER JOIN\r\n                         People ON Applications.ApplicantPersonID = People.PersonID\r\n\t\t\t\t\t\t where People.PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


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

        public static DataTable GetAllInternationalLicense()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "SELECT   [I.D.L.ID] =  InternationalLicenses.InternationalLicenseID,\r\n[Application ID] = InternationalLicenses.ApplicationID,\r\n[L.License.ID] = InternationalLicenses.IssuedUsingLocalLicenseID,\r\n[Issue Date] = InternationalLicenses.IssueDate,\r\n[Expiration Date] = InternationalLicenses.ExpirationDate,\r\n[Is Active] = InternationalLicenses.IsActive     \r\nFROM            InternationalLicenses INNER JOIN\r\n                         Applications ON InternationalLicenses.ApplicationID = Applications.ApplicationID ";
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
        public static bool Find(int InternationalLicenseID,
           ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,
           ref int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                result = false;
            }
            finally
            { connection.Close(); }


            return result;
        }

        public static bool AddNewInternationalLicense(ref int InternationalLicenseID,
            int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive,
            int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "insert into InternationalLicenses \r\nvalues\r\n(\r\n@ApplicationID,\r\n@DriverID,\r\n@IssuedUsingLocalLicenseID,\r\n@IssueDate,\r\n@ExpirationDate,\r\n@IsActive,\r\n@CreatedByUserID) select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null) 
                {
                    InternationalLicenseID = Convert.ToInt32(obj);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                result = false;
            }
            finally
            { connection.Close(); }


            return result;
        }

        public static bool Update(int InternationalLicenseID,
            int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive,
            int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "update InternationalLicenses\r\nset\r\nApplicationID = @ApplicationID,\r\nDriverID = @DriverID,\r\nIssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,\r\nIssueDate = @IssueDate,\r\nExpirationDate = @ExpirationDate,\r\nIsActive = @IsActive,\r\nCreatedByUserID = @CreatedByUserID\r\nwhere InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                if (RowsAffected >0)
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
            { connection.Close(); }


            return result;
        }

        public static bool Delete(int InternationalLicenseID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "delete from InternationalLicenses\r\nwhere InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);

            
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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
            finally
            { connection.Close(); }


            return result;
        }

    }

}
