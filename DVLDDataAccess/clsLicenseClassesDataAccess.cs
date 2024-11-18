using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsLicenseClassesDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        public static bool FindLicenseClass(int LicenseClassID,
            ref string ClassName, ref string ClassDescription,
            ref byte MinimumAllowedAge, ref byte DefaultValidityLength,
            ref double ClassFees)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from LicenseClasses where LicenseClassID  = @LicenseClassID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ClassName = Convert.ToString(reader["ClassName"]);
                    ClassDescription = Convert.ToString(reader["ClassDescription"]);
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToDouble(reader["ClassFees"]);
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
