using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsLicenseDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static bool FindLicense(int LicenseID
            , ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate,
            ref string Notes, ref double PaidFees, ref bool IsActive,
            ref byte IssueReason, ref int CreatedByUserID)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from Licenses\r\nwhere LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    if (reader["Notes"] == DBNull.Value)
                    {

                        Notes = string.Empty;
                    }
                    else
                    {
                        Notes = Convert.ToString(reader["Notes"]);
                    }

                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);


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

        public static bool AddNewLicense(ref int LicenseID
            , int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate,
             string Notes, double PaidFees, bool IsActive,
            byte IssueReason, int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "INSERT INTO Licenses\r\n           \r\n     VALUES\r\n           (@ApplicationID\r\n           ,@DriverID\r\n           ,@LicenseClass\r\n           ,@IssueDate\r\n           ,@ExpirationDate\r\n           ,@Notes \r\n           ,@PaidFees\r\n           ,@IsActive\r\n           ,@IssueReason\r\n           ,@CreatedByUserID) select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (Notes == string.Empty)
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@Notes", Notes);
            }
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
                {
                    LicenseID = Convert.ToInt32(obj);
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

        public static bool UpdateLicenseToNotActive(int LicenseID
            , int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate,
             string Notes, double PaidFees, bool IsActive,
            byte IssueReason, int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "UPDATE Licenses\r\n   SET ApplicationID = @ApplicationID, DriverID = @DriverID," +
                "  LicenseClass = @LicenseClass,IssueDate = @IssueDate, " +
                "ExpirationDate = @ExpirationDate , Notes = @Notes ," +
                " PaidFees = @PaidFees , [IsActive] = @IsActive , " +
                "IssueReason = @IssueReason ,CreatedByUserID = @CreatedByUserID      WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (Notes == string.Empty || Notes == "")
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@Notes", Notes);
            }
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);



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
            {
                connection.Close();
            }


            return result;
        }

        public static bool CheckIfPersonHasAlreadyLicenseInTheLicenseClass(int PersonID, int LicenseClassID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT        Licenses.LicenseID\r\nFROM            Licenses INNER JOIN\r\n                         Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN\r\n                         People ON Drivers.PersonID = People.PersonID\r\n\t\t\t\t\t\t where People.PersonID = @PersonID and  LicenseClass = @LicenseClassID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
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

        public static DataTable GetAllLocalPersonLicense(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT        [Lic.ID] = Licenses.LicenseID,[App ID] = Licenses.ApplicationID,\r\n[Class Name] = LicenseClasses.ClassName,[Issue Date] = Licenses.IssueDate,\r\n[Expiration Date] = Licenses.ExpirationDate,[Is Active] = Licenses.IsActive\r\nFROM            Licenses INNER JOIN\r\n                         Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN\r\n                         People ON Drivers.PersonID = People.PersonID inner join\r\n\t\t\t\t\t\t LicenseClasses on Licenses.LicenseClass = LicenseClasses.LicenseClassID\r\n\t\t\t\t\t\t where people.PersonID = @PersonID";
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

        public static bool CheckIfLicenseIsDetained(ref int DetainID, int LicenseID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select DetainID from DetainedLicenses\r\nwhere LicenseID = @LicenseID and IsReleased = 0";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
                {
                    DetainID = Convert.ToInt32(obj);
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
