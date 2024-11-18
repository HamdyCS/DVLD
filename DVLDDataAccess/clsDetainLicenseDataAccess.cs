using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace DVLDDataAccess
{
    public static  class clsDetainLicenseDataAccess
    {
        public static bool Find(int DetainID,ref int LicenseID,
            ref DateTime DetainDate,ref double FineFees,
            ref int CreatedByUserID,ref bool IsReleased,
            ref DateTime ReleaseDate ,ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * from DetainedLicenses where DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read()) 
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToDouble(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if(reader["ReleaseDate"]==DBNull.Value)
                    {
                        ReleaseDate = DateTime.MinValue;
                    }
                    else
                    {
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    }

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = 0;
                    }
                    else
                    {
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                    }

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleaseApplicationID = 0;
                    }
                    else
                    {
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
                    }

                    result = true;
                }

            }
            catch(Exception ex) 
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

        public static bool AddNewDetainLicense(ref int DetainID,  int LicenseID,
             DateTime DetainDate,  double FineFees,
             int CreatedByUserID,  bool IsReleased,
             DateTime ReleaseDate,  int ReleasedByUserID,
             int ReleaseApplicationID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "insert into DetainedLicenses \r\nvalues\r\n( @LicenseID , @DetainDate , @FineFees , @CreatedByUserID ,\r\n @IsReleased , @ReleaseDate , @ReleasedByUserID , @ReleaseApplicationID )\r\n select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            if(ReleaseDate == DateTime.MinValue)
            {
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);


            }
            
            if(ReleasedByUserID<1)
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

            }

            if (ReleaseApplicationID<1)
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            }


            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();

                if (obj!= null)
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

        public static bool UpdateDetainLicense( int DetainID, int LicenseID,
             DateTime DetainDate, double FineFees,
             int CreatedByUserID, bool IsReleased,
             DateTime ReleaseDate, int ReleasedByUserID,
             int ReleaseApplicationID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "update DetainedLicenses set \r\n LicenseID = @LicenseID , DetainDate = @DetainDate , FineFees = @FineFees ,\r\n  CreatedByUserID = @CreatedByUserID , IsReleased = @IsReleased , \r\n  ReleaseDate = @ReleaseDate , ReleasedByUserID = @ReleasedByUserID ,\r\n  ReleaseApplicationID = @ReleaseApplicationID \r\n  where DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            if (ReleaseDate == DateTime.MinValue)
            {
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);


            }

            if (ReleasedByUserID < 1)
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

            }

            if (ReleaseApplicationID < 1)
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            }


            try
            {
                connection.Open();
                int RowsAffetcd = command.ExecuteNonQuery();

                if (RowsAffetcd>0)
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

        public static bool DeleteDetainLicense(int DetainID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "delete from DetainedLicenses\r\n  where DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            
            try
            {
                connection.Open();
                int RowsAffetcd = command.ExecuteNonQuery();

                if (RowsAffetcd > 0)
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

        public static DataTable GetAllDetainLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * from MyDetainedLicensesView";

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

                dt = null;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

    }
}
