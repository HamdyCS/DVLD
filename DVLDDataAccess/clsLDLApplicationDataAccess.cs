using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsLDLApplicationDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static bool FindLDLApplication(int LocalDrivingLicenseApplicationID,
            ref int ApplicationID, ref int LicenseClassID)
        {
            bool found = false;

            string Query = "select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID\r\n";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                }



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

        public static bool AddNewLDLApplication(ref int LocalDrivingLicenseApplicationID,
            int ApplicationID, int LicenseClassID)
        {
            bool result = false;

            string Query = "insert into LocalDrivingLicenseApplications\r\nvalues(@ApplicationID,@LicenseClassID); select SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int ID))
                {
                    LocalDrivingLicenseApplicationID = ID;
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

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,
            int ApplicationID, int LicenseClassID)
        {
            bool result = false;

            string Query = "UPDATE LocalDrivingLicenseApplications\r\n   SET [ApplicationID] = @ApplicationID\r\n      ,[LicenseClassID] = @LicenseClassID WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID\r\n";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            bool result = false;

            string Query = "delete from LocalDrivingLicenseApplications\r\nwhere LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static DataTable GetAllLicenseClassName()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select ClassName from LicenseClasses\r\n";

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

        public static bool CheckIfPersonHasActiveApplicationInThisLicenseClass
            (ref int ApplicationID, int LicenseClassID, int PersonID)
        {
            bool result = true;

            string Query = "SELECT        Applications.ApplicationID\r\nFROM            LocalDrivingLicenseApplications INNER JOIN\r\n                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN\r\n                         ApplicationTypes ON Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID INNER JOIN\r\n                         People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN\r\n                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID\r\nWhere LicenseClasses.LicenseClassID =  @LicenseClassID and People.PersonID = @PersonID and Applications.ApplicationStatus = 1 ";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    result = true;
                }
                else
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

        public static DataTable GetAllLocalDrivingApplications()
        {
            DataTable dt = new DataTable();

            string Query = "select * from MyLocalDrivingLicenseApplicationsView";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                dt.Load(reader);

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

        public static DataTable GetAllLDLAppplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from MyAllLocalDrivingLicenseApplicationsInfoView\r\nwhere LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static int GetLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int LicenseID = 0;

            string Query = "SELECT   Licenses.LicenseID \r\nFROM            Licenses INNER JOIN\r\n                         Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN\r\n                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID\r\n\t\t\t\t\t\t where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);




            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
                {
                    LicenseID = Convert.ToInt32(obj);
                }

            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                LicenseID = 0;
            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }
    }

}
