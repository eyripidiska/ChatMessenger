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
                        MainApplication.CreateUserMethod();
                        break;
                    case "b":
                        MainApplication.ViewUserMethod();
                        break;
                    case "c":
                        MainApplication.DeleteMethod();
                        break;
                    case "d":
                        MainApplication.UpdatePasswordMethod();
                        break;
                    case "e":
                        MainApplication.UpdateRoleMethod();
                        break;
                    default:
                        Console.Write("\n");
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        break;
                }
                Console.Write("\n");
            }
        }



        public static string RoleMethod()
        {
            bool CorrectRole = false;
            string role = "";
            while (CorrectRole == false)
            {
                CorrectRole = true;
                Console.WriteLine("\n");
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("To set user privilege press {a}");
                Console.WriteLine("To set Admin with view privilege press {b}");
                Console.WriteLine("To set Admin with view and edit privilege press {c}");
                Console.WriteLine("To set Admin with view, Edit and Delete privilege press {d}");
                Console.WriteLine("To set super Admin privilege press {e}");
                Console.Write("\n");
                Console.Write("Press a letter: ");
                switch (Console.ReadLine())
                {
                    case "a":
                        role = "User";
                        break;
                    case "b":
                        role = "View Admin";
                        break;
                    case "c":
                        role = "View-Edit Admin";
                        break;
                    case "d":
                        role = "View-Edit-Delete Admin";
                        break;
                    case "e":
                        role = "Super Admin";
                        break;
                    default:
                        Console.Write("\n");
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        CorrectRole = false;
                        break;
                }
            }
            return role;
        }



        public static void MessageMethod()
        {
            while (true)
            {
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("To view the users press {a}");
                Console.WriteLine("To choase a user press {b}");
                Console.Write("\n");
                Console.Write("Press a letter: ");
                switch (Console.ReadLine())
                {
                    case "a":
                        MainApplication.ViewUserMethod();
                        break;
                    case "b":
                        string cmd = "select * from users";
                        IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
                        Console.Write("Type the user you want to exchange messages: ");
                        string username = Console.ReadLine();
                        bool existUser = LoginScreen.ReturnExistUser(users, username);
                        if (existUser == true)
                        {
                            Console.WriteLine("evrika");
                        }
                        break;
                    default:
                        Console.Write("\n");
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        break;
                }
                Console.Write("\n");
            }
        }
    }
}
