using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;

namespace ChatMessenger
{
    class Program
    {
        static string connectionString = "Server=localhost\\SQLEXPRESS02; Database=ChatDB; Trusted_Connection = True;";



        static void Main(string[] args)
        {
            LoginMethod();
        }



        static void LoginMethod()
        {
            bool found = false;

            Console.WriteLine("Login Screen");
            Console.WriteLine("\n");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("\n");

            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var user = dbcon.Query("select * from users");

                while (found == false)
                {
                    foreach (var u in user)
                    {
                        if (username == u.username)
                        {
                            string correctPassword = u.pass;
                            int salt = u.salt;
                            //close connection
                            dbcon.Close();
                            found = PasswordMethod(correctPassword, salt);
                            Console.WriteLine("you are enter in");
                            Console.WriteLine("\n");
                            ApplicationMethod();
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("The user doesn't exists");
                        Console.Write("Username: ");
                        username = Console.ReadLine();
                        Console.WriteLine("\n");
                    }
                }
            }
        }



        static bool PasswordMethod(string correctPassword, int salt)
        {
            Console.WriteLine("maskpassword");
            bool check = false;

            while (check == false)
            {
                string password = MaskMethod();
                Console.WriteLine("\n");
                string encryptedPassword = salt.ToString() + password;

                // step 1, calculate MD5 hash from input
                MD5 md5 = System.Security.Cryptography.MD5.Create();

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(encryptedPassword);

                byte[] hash = md5.ComputeHash(inputBytes);

                // step 2, convert byte array to hex string
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)

                {
                    sb.Append(hash[i].ToString("X2"));
                }
                encryptedPassword = sb.ToString();

                if (encryptedPassword != correctPassword)
                {
                    Console.WriteLine("Incorrect password try again");
                    password = "";
                }
                else
                {
                    check = true;
                }
            }
            Console.WriteLine("end");
            return check;
        }



        static string MaskMethod()
        {
            string password = "";
            Console.Write("Password: ");
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                // Skip if Backspace or Enter is Pressed
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Remove last charcter if Backspace is Pressed
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            return password;
        }


        static void ApplicationMethod()
        {
            while (true)
            {
                Console.WriteLine("to create a user press a");
                Console.WriteLine("to view a user press b");
                Console.WriteLine("to delete a user press c");
                Console.WriteLine("to update a user press d");
                Console.Write("Type one letter: ");

                switch (Console.ReadLine())
                {
                    case "a":
                        CreateMethod();
                        break;
                    case "b":
                        ViewMethod();
                        break;
                    case "c":
                        DeleteMethod();
                        break;
                    case "d":
                        UpdateMethod();
                        break;
                    // Return text for an incorrect option entry
                    default:
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        break;
                }
                Console.WriteLine("\n");
            }
        }



        static void CreateMethod()
        {
            SqlConnection dbcon = new SqlConnection(connectionString);
            bool check = false;
            string password1 = "";
            string password2 = "";
            Console.Write("Type the name who want to create: ");
            string username = Console.ReadLine();


            while(check == false)
            {
                Console.Write("Type the password: ");
                password1 = MaskMethod();
                Console.WriteLine("\n");
                Console.Write("Repeat the password: ");
                password2 = MaskMethod();
                if (password1 == password2)
                {
                    check = true;
                }
                else
                {
                    Console.WriteLine("the passwords doesn't match");
                }
            }
            Console.WriteLine("\n");
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@pass", password1);

                var affectedRows = dbcon.Execute("Insert_Users", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
        }



        static void ViewMethod()
        {
            SqlConnection dbcon = new SqlConnection(connectionString);
            Console.WriteLine("The users are:");
            using (dbcon)
            {
                dbcon.Open();
                var user = dbcon.Query("select * from users");
                foreach (var u in user)
                {
                    Console.WriteLine(u.username);
                }
            }
            Console.WriteLine("Press any key to return back");
            Console.WriteLine("\n");
            Console.ReadKey();
        }


        static void DeleteMethod()
        {
            SqlConnection dbcon = new SqlConnection(connectionString);
            Console.Write("Type the name who want to delete: ");
            string username = Console.ReadLine();
            using (dbcon)
            {
                dbcon.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                var affectedRows = dbcon.Execute("Delete_Users", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"{affectedRows} Affected Rows");
            }
        }

        static void UpdateMethod()
        {

        }
        
    }
}
