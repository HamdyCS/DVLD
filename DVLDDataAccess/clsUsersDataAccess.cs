using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DVLDDataAccess
{
    static public class clsUsersDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;

        static  string FilePath = @"C:\RemeberUserFile\RemeberUserFile.txt";

        static string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\MyProjects\DVLD";

        static string ValueName = @"LoginInfo";

        static char Separator = '-';

        static string FolderPath = @"C:\RemeberUserFile";
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
            catch(Exception ex) 
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
            catch( Exception ex) 
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
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                Result = false;

            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool GetUserInfoFromWindowsRegistry(ref string userName, ref string password)
        {
            bool found = false;

            /*if (!File.Exists(FilePath))
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
            }*/

            try
            {
                string Value = Registry.GetValue(KeyPath,ValueName,null) as string;
                if (Value != null)
                {
                    string[] arr = Value.Split(Separator);
                    if (arr.Length > 0)
                    {
                        userName = arr[0];
                        password = arr[1];
                        found = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                found = false;
            }

            return found;
        }

        public static bool DeleteAllRecordInRemeberUserFile()
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
                clsSettings.LogErrorInEventLog(ex.Message);

                Result = false;
            }

            return Result;
        }

        public static bool SaveUserInfoInWindowsRegistry(string userName, string password)
        {
            bool Result = false;

            /*if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

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
            }*/

            try
            {
                Registry.SetValue(KeyPath, ValueName, string.Join(Separator.ToString(), userName, password));
                Result = true;
            }
            catch (Exception ex)
            {
                clsSettings.LogErrorInEventLog(ex.Message);

                Result = false;
            }

            return Result;
        }

        public static DataTable GetAllUsersInfo()
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

                clsSettings.LogErrorInEventLog(ex.Message);

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

                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public static bool FindUserByUserName(string UserName, ref int UserID, ref int PersonID
            , ref string Password,
             ref bool IsActive)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "select * from users where UserName = @UserName";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);



            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);                    
                    Password = Convert.ToString(reader["Password"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
