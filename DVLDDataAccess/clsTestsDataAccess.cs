using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestsDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static bool AddNewTest(ref int TestID,
            int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "INSERT INTO [dbo].[Tests]\r\n           ([TestAppointmentID]\r\n           ,[TestResult]\r\n           ,[Notes]\r\n           ,[CreatedByUserID])\r\n     VALUES\r\n           (@TestAppointmentID\r\n           ,@TestResult\r\n           ,@Notes\r\n           ,@CreatedByUserID)\r\n\t\t   select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes == string.Empty)
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@Notes", Notes);
            }
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    TestAppointmentID
                        = Convert.ToInt32(obj);
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
