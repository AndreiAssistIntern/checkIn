using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJob1.Data
{
    class SqlClass
    {
        public SqlClass() { }

        public void Verify()
        {
            //string connectionString = "Server=tcp:andreiserver.database.windows.net,1433;Initial Catalog=andrei;Persist Security Info=False;User ID=andrei;Password=Kirilov21*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select * from AspNetUsers where ISLOGIN = '" + "True" + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            List<bool> isLoggedIn = new List<bool>();
            List<string> userEmail = new List<string>();

            try
            {
                sqlConnection1.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Data is accessible through the DataReader object here.
                    while (reader.Read())
                    {
                        userEmail.Add(reader.GetValue(0).ToString());
                        isLoggedIn.Add(bool.Parse(reader.GetValue(1).ToString()));

                    }
                }
            }

            catch (SqlException e)
            {
                Console.WriteLine("The sql has the folowing error:\n" + e.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine("The sql has the folowing unknow error" + e.Message);
            }

            if (userEmail.Count > 0)
            {
                int param = 0;
                foreach (var user in userEmail)
                {
                    param++;

                    cmd.CommandText = "SELECT * FROM CheckIns where UserId = '" + user + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    string check = "";
                    bool hascheck = false;

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Data is accessible through the DataReader object here.
                            while (reader.Read())
                            {
                                hascheck = (bool)reader.GetValue(2);
                                check = reader.GetValue(3).ToString();

                            }
                            if (check != "")
                            {
                                Console.WriteLine("The date time picker is " + check);

                                //  check = check.Replace("p.m.", "PM"); // run azure
                                //    check = check.Replace("a.m.", "AM"); // run azure
                                  check = check.Replace("PM", "p.m."); // run local
                                  check = check.Replace("AM", "a.m."); // run local
                                Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + " and " + check);
                                DateTime s = DateTime.ParseExact(check, "h:mm:ss tt", null);
                                TimeSpan diference = DateTime.Now - DateTime.ParseExact(check, "h:mm:ss tt", null);

                                //DIFERENCEMINUTES
                                var diffMin = ConfigurationManager.AppSettings["DIFERENCEMINUTES"]; // azure
                                                                                                    //   var diffMin = "1"; // local
                                if (diference.Minutes >= Int32.Parse("1"))
                                {
                                    Console.WriteLine("Attemp to write");
                                    reader.Close();

                                    cmd.CommandText = "Insert into CheckIns(UserId,checkUser,hasChecked) values ('" + user + "','" + DateTime.Now.ToString("h:mm:ss tt") + "','False')";
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = sqlConnection1;

                                    if (param == 1)
                                    {
                                        cmd.Parameters.AddWithValue("@UserId", user);
                                        cmd.Parameters.AddWithValue("@check", DateTime.Now.ToString("h:mm:ss tt").ToString());
                                        cmd.Parameters.AddWithValue("@hasChecked", false);
                                    }
                                    int recordsAffected = cmd.ExecuteNonQuery();

                                    Console.WriteLine(recordsAffected);
                                    Console.WriteLine("Succes trying to write");

                                }
                            }
                        }

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("The sql has the folowing error:\n" + e.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The sql has the folowing unknow error" + e.Message);

                    }
                }
            }
            sqlConnection1.Close();
        }


    }
}
