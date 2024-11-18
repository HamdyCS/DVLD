using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestAppointmentsDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static DataTable GetAllTestAppointmentByLDLAppIDAndTestTypeID
            (int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT        TestAppointments.TestAppointmentID,  TestAppointments.AppointmentDate, TestAppointments.PaidFees,\r\n                         TestAppointments.IsLocked \r\nFROM            LocalDrivingLicenseApplications INNER JOIN\r\n                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID\r\n\t\t\t\t\t\t where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID\r\n\t\t\t\t\t\t and TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static bool AddNewTestAppointment
            (ref int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, double PaidFees,
            int CreatedByUserID, bool IsLocked,
            int RetakeTestApplicationID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "INSERT INTO [dbo].[TestAppointments]\r\n           ([TestTypeID]\r\n           ,[LocalDrivingLicenseApplicationID]\r\n           ,[AppointmentDate]\r\n           ,[PaidFees]\r\n           ,[CreatedByUserID]\r\n           ,[IsLocked]\r\n           ,[RetakeTestApplicationID])\r\n     VALUES\r\n           (@TestTypeID,\r\n           @LocalDrivingLicenseApplicationID,\r\n           @AppointmentDate, \r\n           @PaidFees,\r\n           @CreatedByUserID,\r\n           @IsLocked,\r\n           @RetakeTestApplicationID) select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == 0)
            {

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            }

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
                {
                    TestAppointmentID = Convert.ToInt32(obj);
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

        public static bool UpdateAppointmentActive
           (int TestAppointmentID, bool IsLocked)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "UPDATE [dbo].[TestAppointments]\r\n   SET IsLocked = @IsLocked\r\n WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);



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

        public static bool UpdateAppointmentDate
           (int TestAppointmentID, DateTime AppointmentDate)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "UPDATE [dbo].[TestAppointments]\r\n   SET AppointmentDate = @AppointmentDate\r\n WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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
        public static bool FindAppointment
            (int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref double PaidFees,
           ref int CreatedByUserID, ref bool IsLocked,
           ref int RetakeTestApplicationID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select *  from TestAppointments where TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    {
                        RetakeTestApplicationID = 0;
                    }
                    else
                    {

                        RetakeTestApplicationID = Convert.ToInt32(reader["RetakeTestApplicationID"]);
                    }
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


        public static bool CheckIfThisLDLAppHasActiveTestAppointment
            (int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT TestAppointmentID       \r\nFROM            TestAppointments\r\nwhere LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID\r\nand TestTypeID = @TestTypeID\r\nand IsLocked = 0";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

                result = true;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }




        public static bool CheckIfThisLDLAppPassedThisTest
           (int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT        TestAppointments.TestAppointmentID\r\nFROM            TestAppointments INNER JOIN\r\n                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN\r\n                         TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID\r\n\t\t\t\t\t\t where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID\r\n\t\t\t\t\t\tand TestTypes.TestTypeID = @TestTypeID\r\n\t\t\t\t\t\tand Tests.TestResult = 1";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

                result = true;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }

        public static bool CheckIfThisLDLAppFailedThisTest
            (int LocalDrivingLicenseApplicationID, int TestTypeID, ref int Trial)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT       count( TestAppointments.TestAppointmentID )\r\nFROM            TestAppointments INNER JOIN\r\n                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN\r\n                         TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID\r\n\t\t\t\t\t\t where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID\r\n\t\t\t\t\t\tand TestTypes.TestTypeID = @TestTypeID\r\n\t\t\t\t\t\tand Tests.TestResult = 0";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    Trial = Convert.ToInt32(obj);
                    result = true;
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

    }

}
