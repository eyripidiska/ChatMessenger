using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Security;

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
                            found = PasswordMethod(correctPassword);
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


        static bool PasswordMethod(string correctPassword)
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
                if (password != correctPassword)
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
