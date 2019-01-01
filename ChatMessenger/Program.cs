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
            string password = ""; 
            ConsoleKeyInfo keyInfo;

            while (check == false)
            {
                Console.Write("Password: ");
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
                string finallypassword = sb.ToString();
                if (finallypassword != correctPassword)
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



        static void DatabaseMethod()
        {

        }
        


        static void ApplicationMethod()
        {

        }
    }
}
