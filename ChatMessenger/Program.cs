using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ChatMessenger
{
    class Program
    {
        static string connectionString = "Server=localhost\\SQLEXPRESS02; Database=ChatDB; Trusted_Connection = True;";

        static void Main(string[] args)
        {
            Method1();
            Console.WriteLine("login commit2");
        }

        static void Method1()
        {
            Console.WriteLine("Login Screen");
            Console.Write("Username: ");
            string username = Console.ReadLine();


            SqlConnection dbcon = new SqlConnection(connectionString);
            using (dbcon)
            {
                dbcon.Open();
                var user = dbcon.Query("select * from users");

                foreach (var u in user)
                {
                    if (username == u.username)
                    {
                        Console.Write("Password: ");
                        string password = Console.ReadLine();
                        while (password != u.pass)
                        {
                            Console.WriteLine("Incorrect password try again");
                            Console.Write("Password: ");
                            password = Console.ReadLine();
                        }
                        Console.WriteLine("you are enter in");
                    }
                    else
                    {
                        Console.WriteLine("The user doesn't exists");
                    }
                    Console.WriteLine($"Username: {u.username}, Password: {u.pass}");
                }


            }
        }
    }
}
