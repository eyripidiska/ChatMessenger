using System;
using System.Collections.Generic;

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
                Console.WriteLine("To view the users press {b}");
                Console.WriteLine("To delete a user press {c}");
                Console.WriteLine("To update a user password press {d}");
                Console.WriteLine("To update a user role press {e}");
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
                        UpdatePasswordMethod();
                        break;
                    case "e":
                        UpdateRoleMethod();
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
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine(u.username);
            }
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
        }



        static void DeleteMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if(UserExist == false)
            {
                Console.Write("The user does not exist");
                Console.WriteLine("\n");
            }
            DatabasesAccess.DeleteUsers(username);
        }



        static void UpdatePasswordMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.WriteLine("------------------------------------------------------");
                string password = LoginScreen.SamePasswordMethod();
                DatabasesAccess.UpdatePassword(username, password);
            }
            else
            {
                Console.Write("The user does not exist");
            }
        }



        static void UpdateRoleMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.WriteLine("------------------------------------------------------");
            }
            else
            {
                Console.Write("The user does not exist");
            }
        }
    }
}
