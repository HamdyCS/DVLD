using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public class clsDVLDataAccess
    {
    }

    public static class clsApplicationsDataAccess
    {
        static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

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
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

    }

    public static class clsApplicationTypesDataAccess
    {
        private static string ConnectionString = "server=.;Database=DVLD;Integrated Security=True;";

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
                ApplicationFees = -1;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationFees;
        }


    }

    public class clsDriversDataAccess
    {
        static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

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
                found = false;
            }
            finally
            {
                connection.Close();

            }

            return found;
        }
    }

    public static class clsInternationalLicenseDataAccess
    {
        private static string ConnectionString = "server=.;Database=DVLD;Integrated Security=True;";

        public static DataTable GetAllInternationalPersonLicense(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT   [Int.License.ID] =  InternationalLicenses.InternationalLicenseID,\r\n[Application ID] = InternationalLicenses.ApplicationID,\r\n[L.License.ID] = InternationalLicenses.IssuedUsingLocalLicenseID,\r\n[Issue Date] = InternationalLicenses.IssueDate,\r\n[Expiration Date] = InternationalLicenses.ExpirationDate,\r\n[Is Active] = InternationalLicenses.IsActive     \r\nFROM            InternationalLicenses INNER JOIN\r\n                         Applications ON InternationalLicenses.ApplicationID = Applications.ApplicationID INNER JOIN\r\n                         People ON Applications.ApplicantPersonID = People.PersonID\r\n\t\t\t\t\t\t where People.PersonID = @PersonID";
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

            }
            finally
            {
                connection.Close();
            }


            return dt;
        }
    }

    public static class clsLDLApplicationDataAccess
    {
        static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

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

            }
            finally
            {
                connection.Close();
            }


            return dt;
        }

        public static bool CheckIfPersonHasActiveApplicationWithTheSameLicenseClassName
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
                LicenseID = 0;
            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }
    }

    public static class clsLicenseClassesDataAccesscs
    {
        private static string ConnectionString = "server=.;Database=DVLD;Integrated Security=True;";

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
                found = false;
            }
            finally { connection.Close(); }


            return found;
        }
    }

    public static class clsLicenseDataAccess
    {
        private static string ConnectionString = "server=.;Database=DVLD;Integrated Security=True;";

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
                result = false;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }

        public static bool UpdateLicense(int LicenseID
            , int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate,
             string Notes, double PaidFees, bool IsActive,
            byte IssueReason, int CreatedByUserID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "UPDATE Licenses\r\n   SET [ApplicationID] = @ApplicationID\r\n      ,[DriverID] = @DriverID\r\n      ,[LicenseClass] = @LicenseClass\r\n      ,[IssueDate] = @IssueDate\r\n      ,[ExpirationDate] = @ExpirationDate\r\n      ,[Notes] = @Notes\r\n      ,[PaidFees] = @PaidFees\r\n      ,[IsActive] = @IsActive\r\n      ,[IssueReason] = @IssueReason\r\n      ,[CreatedByUserID] = @CreatedByUserID\r\n WHERE LicenseID = @LicenseID";
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
            command.Parameters.AddWithValue("@LicenseID", LicenseClass);



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

            }
            finally
            {
                connection.Close();
            }


            return dt;
        }

        public static bool CheckIfLicenseIsDetained(int LicenseID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select DetainID from DetainedLicenses\r\nwhere LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


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
                result = false;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }

    }

    public static class clsPeoplesDataAcess
    {
        static string ConnectionString = "server = .;database = DVLD; user Id = sa; password = sa123456 ";
        public static DataTable GetLittleInfoAboutPeople()
        {

            //Person ID
            //National No
            //First Name
            //Second Name
            //Third Name
            //Nationalily
            //Gendor
            //Phone
            //Email

            DataTable table = new DataTable();
            table.Columns.Add("Person ID", typeof(int));
            table.Columns.Add("National No", typeof(string));
            table.Columns.Add("First Name", typeof(string));
            table.Columns.Add("Second Name", typeof(string));
            table.Columns.Add("Third Name", typeof(string));
            table.Columns.Add("Last Name", typeof(string));
            table.Columns.Add("Nationalily", typeof(string));
            table.Columns.Add("Gendor", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Email", typeof(string));


            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, \r\nGendor =\r\ncase\r\nWhen People.Gendor = 0 then 'Male'\r\nWhen People.Gendor = 1 then 'Female'\r\nelse 'Not Set'\r\nEnd\r\n\r\n,People.DateOfBirth, Countries.CountryName, People.Phone, People.Email\r\nFROM            People INNER JOIN\r\n                         Countries ON People.NationalityCountryID = Countries.CountryID\r\n\r\n";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // table.Load(reader);
                    table.Rows.Add(reader["PersonID"],
                        reader["NationalNo"], reader["FirstName"],
                        reader["SecondName"], reader["ThirdName"]
                        , reader["LastName"], reader["CountryName"],
                        reader["Gendor"], reader["Phone"],
                        reader["Email"]);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return table;
        }

        public static bool FindPersonByID(int PersonID, ref string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref DateTime DateOfBirth, ref int Gendor,
            ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
            ref string ImagePath)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "select * from People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NationalNo = Convert.ToString(reader["NationalNo"]);
                    FirstName = Convert.ToString(reader["FirstName"]);
                    SecondName = Convert.ToString(reader["SecondName"]);
                    ThirdName = Convert.ToString(reader["ThirdName"]);
                    LastName = Convert.ToString(reader["LastName"]);
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = Convert.ToString(reader["Address"]);
                    Phone = Convert.ToString(reader["Phone"]);
                    Email = Convert.ToString(reader["Email"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = Convert.ToString(reader["ImagePath"]);

                    found = true;
                }

            }
            catch (Exception ex)
            {
                found = false;
            }
            finally
            {
                connection.Close();
            }


            return found;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo,
             string FirstName, string SecondName, string ThirdName,
             string LastName, DateTime DateOfBirth, int Gendor,
             string Address, string Phone, string Email, int NationalityCountryID,
             string ImagePath)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "UPDATE [dbo].[People]\r\n   SET [NationalNo] = @NationalNo\r\n      ,[FirstName] = @FirstName\r\n      ,[SecondName] = @SecondName\r\n      ,[ThirdName] = @ThirdName\r\n      ,[LastName] = @LastName\r\n      ,[DateOfBirth] = @DateOfBirth\r\n      ,[Gendor] = @Gendor\r\n      ,[Address] = @Address\r\n      ,[Phone] = @Phone\r\n      ,[Email] = @Email\r\n      ,[NationalityCountryID] = @NationalityCountryID\r\n      ,[ImagePath] = @ImagePath\r\n WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            //command.Parameters.AddWithValue("@NationalNo", NationalNo);
            //command.Parameters.AddWithValue("@FirstName", FirstName);
            //command.Parameters.AddWithValue("@SecondName", SecondName);
            //command.Parameters.AddWithValue("@ThirdName", ThirdName);
            //command.Parameters.AddWithValue("@LastName", LastName);
            //command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            //command.Parameters.AddWithValue("@Gendor", Gendor);
            //command.Parameters.AddWithValue("@Address", Address);
            //command.Parameters.AddWithValue("@Phone", Phone);
            //command.Parameters.AddWithValue("@Email", Email);
            //command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            //command.Parameters.AddWithValue("@ImagePath", ImagePath);
            //command.Parameters.AddWithValue("@PersonID", PersonID);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (string.IsNullOrEmpty(ThirdName))
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);



            if (string.IsNullOrEmpty(Email))
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@Email", Email);
            }

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);


            if (string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                int RowsEffected = command.ExecuteNonQuery();

                if (RowsEffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }

        public static bool AddNewPerson(ref int PersonID, string NationalNo,
             string FirstName, string SecondName, string ThirdName,
             string LastName, DateTime DateOfBirth, int Gendor,
             string Address, string Phone, string Email, int NationalityCountryID,
             string ImagePath)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "INSERT INTO [dbo].[People]\r\n           ([NationalNo]\r\n           ,[FirstName]\r\n           ,[SecondName]\r\n           ,[ThirdName]\r\n           ,[LastName]\r\n           ,[DateOfBirth]\r\n           ,[Gendor]\r\n           ,[Address]\r\n           ,[Phone]\r\n           ,[Email]\r\n           ,[NationalityCountryID]\r\n           ,[ImagePath])\r\n     VALUES\r\n           (@NationalNo\r\n           ,@FirstName\r\n           ,@SecondName\r\n           ,@ThirdName\r\n           ,@LastName\r\n           ,@DateOfBirth\r\n           ,@Gendor\r\n           ,@Address\r\n           ,@Phone\r\n           ,@Email\r\n           ,@NationalityCountryID\r\n           ,@ImagePath)\r\n\t\t   ; select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (string.IsNullOrEmpty(ThirdName))
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);



            if (string.IsNullOrEmpty(Email))
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@Email", Email);
            }

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);


            if (string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int ID))
                {
                    PersonID = ID;
                    result = true;
                }


            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }




            return result;
        }

        public static bool DeletePerson(int PersonID)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "DELETE FROM [dbo].[People]\r\n      WHERE PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();

                int RowsEffeted = command.ExecuteNonQuery();

                if (RowsEffeted > 0)
                {
                    result = true;
                }


            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }




            return result;
        }

        public static DataTable GetAllCountriesName()
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select Countries.CountryName from Countries";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    table.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }



            return table;
        }

        //public static int GetNumberOfPeopleInSystem()
        //{
        //    int result = 0;

        //    SqlConnection connection = new SqlConnection(ConnectionString);

        //    string query = "select count(People.PersonID) from People;";
        //    SqlCommand command = new SqlCommand(query, connection);




        //    try
        //    {
        //        connection.Open();

        //        object obj = command.ExecuteScalar();

        //        if (obj != null && int.TryParse(obj.ToString(), out int num))
        //        {

        //            result = num;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        result = 0;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }




        //    return result;
        //}

        public static bool IsNationalNoInSystem(string NationalNo)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "select * from People\r\nwhere People.NationalNo = @NationalNo;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    found = true;
                }

            }
            catch (Exception ex)
            {
                found = false;
            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public static string GetCountryNameByPersonID(int personID)
        {
            string CountyName = "";


            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT        Countries.CountryName\r\nFROM            People INNER JOIN\r\n                         Countries ON People.NationalityCountryID = Countries.CountryID\r\n\t\t\t\t\t\t where People.PersonID = @PersonID\r\n\t\t\t\t\t\t";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);


            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                CountyName = (string)obj;



            }
            catch (Exception ex)
            {
                CountyName = ""; ;
            }
            finally
            {
                connection.Close();
            }

            return CountyName;
        }













    }

    public static class clsTestAppointmentsDataAccess
    {
        static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

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
                result = true;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }

    }

    public static class clsTestsDataAccess
    {
        static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

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
                result = false;
            }
            finally
            {
                connection.Close();
            }


            return result;
        }
    }

    public static class clsTestTypesDataAccess
    {
        private static string ConnectionString = "server=.;Database=DVLD;Integrated Security=True;";

        public static DataTable GetAllTestTypesContent()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Fees", typeof(double));

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from TestTypes";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dt.Rows.Add(reader["TestTypeID"],
                        reader["TestTypeTitle"],
                        reader["TestTypeDescription"],
                        reader["TestTypeFees"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }


            return dt;
        }

        public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle,
           string TestTypeDescription, double TestTypeFees)

        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "update TestTypes set TestTypeTitle =  " +
                "@TestTypeTitle, TestTypeDescription = @TestTypeDescription , " +
                "TestTypeFees = @TestTypeFees " +
                "where TestTypeID = @TestTypeID ";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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
                result = false;
            }
            finally { connection.Close(); }


            return result;
        }

        public static bool FindTestTypeByID(int TestTypeID, ref string TestTypeTitle,
           ref string TestTypeDescription, ref double TestTypeFees)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from TestTypes where TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeTitle = Convert.ToString(reader["TestTypeTitle"]);
                    TestTypeDescription = Convert.ToString(reader["TestTypeDescription"]);
                    TestTypeFees = Convert.ToDouble(reader["TestTypeFees"]);
                    found = true;
                }

            }
            catch (Exception ex)
            {
                found = false;
            }
            finally { connection.Close(); }



            return found;
        }
    }

    static public class clsUsersDataAccess
    {
        static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

        static string FilePath = "C:\\RemeberUserFile\\RemeberUserFile.txt";

        public static bool CheckIfUserNameInSystem(string userName)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select UserID from users where UserName = @UserName";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", userName);

            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();
                if (obj != null)
                {
                    found = true;
                }
            }
            catch
            {
                found = false;

            }
            finally
            {
                connection.Close();
            }

            return found;
        }

        public static bool CheckIfUserNameAndPasswordIsTrue(string userName, string password)
        {
            bool found = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select UserID from users where UserName = @UserName and Password = @password";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@password", password);



            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();
                if (obj != null)
                {
                    found = true;
                }
            }
            catch
            {
                found = false;

            }
            finally
            {
                connection.Close();
            }

            return found;
        }

        public static bool CheckIfUserIsActive(string userName)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select UserID from users\r\nwhere UserName = @UserName and  IsActive = 1 ";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", userName);




            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();
                if (obj != null)
                {
                    Result = true;
                }
            }
            catch
            {
                Result = false;

            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool GetUserInfoInRemeberUserFile(ref string userName, ref string password)
        {
            bool found = false;

            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                // Specify the path to your text file


                // Read all lines from the file
                string[] lines = File.ReadAllLines(FilePath);

                // Output each line to the console

                if (lines.Length == 0)
                {
                    found = false;
                    return found;
                }

                foreach (string line in lines)
                {
                    string[] UserInfo = line.Split('-');

                    userName = UserInfo[0];
                    password = UserInfo[1];

                    found = true;

                    return found;

                }
            }
            catch (Exception ex)
            {
                found = false;
            }

            return found;
        }

        public static bool DeleteAllRecordRemeberUserFile()
        {
            bool Result = false;

            try
            {

                // Open the file in write mode with FileMode.Truncate
                using (FileStream fs = File.Open(FilePath, FileMode.Truncate))
                {
                    // Truncate the file by setting its length to 0
                    fs.SetLength(0);
                }

                Result = true;

            }
            catch (Exception ex)
            {
                Result = false;
            }

            return Result;
        }

        public static bool SaveUserInfoRemeberUserFile(string userName, string password)
        {
            bool Result = false;


            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }

            try
            {
                string Record = userName + '-' + password;
                // Open the file in write mode with FileMode.Truncate
                using (StreamWriter sw = File.AppendText(FilePath))
                {
                    sw.WriteLine(Record);
                }

                Result = true;

            }
            catch (Exception ex)
            {
                Result = false;
            }

            return Result;
        }

        public static DataTable GetAllUserInfo()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("User ID", typeof(int));
            dt.Columns.Add("Person ID", typeof(int));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("UserName", typeof(string)); ;
            dt.Columns.Add("Is Active", typeof(bool));

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT        Users.UserID, Users.PersonID,\r\n\r\nFullName = People.FirstName + ' '+ People.SecondName + ' '+ People.ThirdName + ' ' + People.LastName\r\n,Users.UserName, Users.IsActive\r\nFROM            Users INNER JOIN\r\n                         People ON Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dt.Rows.Add(reader["UserID"], reader["PersonID"],
                   reader["FullName"], reader["UserName"], reader["IsActive"]);

                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetUserInfo(string UserName)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from users \r\nwhere UserName = @UserName";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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


            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool FindUserByUserID(int UserID, ref int PersonID,
            ref string UserName, ref string Password,
             ref bool IsActive)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from users where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);



            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = Convert.ToString(reader["UserName"]);
                    Password = Convert.ToString(reader["Password"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    result = true;
                }

            }
            catch (Exception ex)
            {

                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public static bool AddNewUserByPersonID(int PersonID, ref int UserID,
            string UserName, string Password,
            bool IsActive)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "INSERT INTO [dbo].[Users]\r\n           ([PersonID]\r\n           ,[UserName]\r\n           ,[Password]\r\n           ,[IsActive])\r\n     VALUES\r\n           (@PersonID\r\n           ,@UserName\r\n           ,@Password\r\n           ,@IsActive) select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                connection.Open();

                object obj = command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int ID))
                {
                    UserID = ID;
                    result = true;
                }

            }
            catch (Exception ex)
            {

                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public static bool UpdateUserByUserID(int UserID, string UserName,
            string Password,
            bool IsActive)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "UPDATE [dbo].[Users]\r\n   SET [UserName] = @UserName\r\n      ,[Password] = @Password\r\n      ,[IsActive] = @IsActive\r\n WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);




            try
            {
                connection.Open();

                int rowsAffeted = command.ExecuteNonQuery();

                if (rowsAffeted > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {

                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public static bool DeleteUserByUserID(int UserID)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "DELETE FROM [dbo].[Users]\r\n      WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                connection.Open();

                int rowsAffeted = command.ExecuteNonQuery();

                if (rowsAffeted > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {

                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public static bool CheckIfThisPersonIsUserByPersonID(int PersonID)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select UserID from users\r\nwhere PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                // عشان يوقف العملية بتاعت الاضافة
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
