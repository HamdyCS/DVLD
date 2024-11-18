using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsPeoplesDataAccess
    {
        static string ConnectionString = clsSettings.ConnectionString;
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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

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
                clsSettings.LogErrorInEventLog(ex.Message);

                CountyName = ""; ;
            }
            finally
            {
                connection.Close();
            }

            return CountyName;
        }


    }
}
