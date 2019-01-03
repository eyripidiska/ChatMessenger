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

    class ApplicationsMenus
    {
        public static void ApplicationMethod()
        {
            while (true)
            {
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("To create a user press {a}");
                Console.WriteLine("To view a user press {b}");
                Console.WriteLine("To delete a user press {c}");
                Console.WriteLine("To update a user press {d}");
                Console.Write("\n");
                Console.Write("Press a letter: ");

                switch (Console.ReadLine())
                {
                    case "a":
                        CreateUserMethod();
                        break;
                    case "b":
                        ViewUserMethod();
                        break;
                    case "c":
                        DeleteMethod();
                        break;
                    case "d":
                        UpdateMethod();
                        break;
                    // Return text for an incorrect option entry
                    default:
                        Console.Write("\n");
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        break;
                }
                Console.Write("\n");
            }
        }



        static void CreateUserMethod()
        {
            string cmd = "select * from users";

            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            string username = LoginScreen.CheckExistUser(users);
            string password = LoginScreen.SamePasswordMethod();
            DatabasesAccess.InsertUsers(username, password);
        }



        static void ViewUserMethod()
        {
            string cmd = "select * from users";

            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);

            Console.WriteLine("The users are:");

            foreach (var u in users)
            {
                Console.WriteLine(u.username);
            }
            Console.WriteLine("Press any key to return back");
            Console.WriteLine("\n");
            Console.ReadKey();
        }


        static void DeleteMethod()
        {
            var connectionString = Properties.Settings.Default.connectionString;
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
            var connectionString = Properties.Settings.Default.connectionString;
            string Case = "c";
            string password1 = "";
            string password2 = "";
            string newUsername = "";
            bool foundUser = false;
            string username = "";
            SqlConnection dbcon = new SqlConnection(connectionString);
            SqlConnection dbcon2 = new SqlConnection(connectionString);
            Console.Write("Type the name who want to update: ");
            username = Console.ReadLine();
            using (dbcon)
            {
                dbcon.Open();
                var user = dbcon.Query("select * from users");

                while (foundUser == false)
                {
                    foreach (var u in user)
                    {
                        if (username == u.username)
                        {
                            foundUser = true;
                        }
                    }
                    if (foundUser == false)
                    {
                        Console.WriteLine("The name doesnt fount");
                        Console.Write("Type the name who want to update: ");
                        username = Console.ReadLine();
                    }
                }
            }
            Console.WriteLine("\n");
            Console.WriteLine("for username type a");
            Console.WriteLine("for password type b");
            Console.Write("Type the field who want to update: ");
            while (Case == "c")
            {
                switch (Console.ReadLine())
                {
                    case "a":
                        Console.Write("Type the new username: ");
                        newUsername = Console.ReadLine();
                        Case = "a";
                        break;
                    case "b":
                        bool check = false;
                        while (check == false)
                        {
                            Console.Write("Type the new password: ");
                            password1 = LoginScreen.MaskMethod();
                            Console.WriteLine("\n");
                            Console.Write("Repeat the new password: ");
                            password2 = LoginScreen.MaskMethod();
                            if (password1 == password2)
                            {
                                check = true;
                            }
                            else
                            {
                                Console.WriteLine("the passwords doesn't match");
                            }
                        }
                        Case = "b";
                        break;
                    default:
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        break;
                }
            }
            Console.WriteLine("\n");
            using (dbcon2)
            {
                dbcon2.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                if (Case == "a")
                {
                    parameters.Add("newUsername", newUsername);
                    var affectedRows = dbcon2.Execute("Update_Users_By_Username", parameters, commandType: CommandType.StoredProcedure);
                    Console.WriteLine($"{affectedRows} Affected Rows");
                }
                else if (Case == "b")
                {
                    parameters.Add("newPassword", password1);
                    var affectedRows = dbcon2.Execute("Update_Users_By_Password", parameters, commandType: CommandType.StoredProcedure);
                    Console.WriteLine($"{affectedRows} Affected Rows");
                }
            }
        }
    }
}
