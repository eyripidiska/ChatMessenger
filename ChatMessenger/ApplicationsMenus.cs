using System;
using System.Collections.Generic;

namespace ChatMessenger
{

    class ApplicationsMenus
    {
        public static void ApplicationMethod(string username, string TypeOfUser)
        {
            Users user;
            Console.WriteLine($"Welcome {username}");
            Console.Write("\n");

            if (TypeOfUser == "Super Admin")
            {
                user = new SuperAdmin(username);
            }
            else if (TypeOfUser == "View-Edit-Delete Admin")
            {
                user = new ViewEditDeleteAdmin(username);
            }
            else if (TypeOfUser == "View-Edit Admin")
            {
                user = new ViewEditAdmin(username);
            }
            else if (TypeOfUser == "View Admin")
            {
                user = new ViewAdmin(username);
            }
            else 
            {
                user = new User(username);
            }
            while (true)
            {
                user.PublicMenuMethod();
                Console.Write("\n");
                Console.Write("Press a letter: ");

                string choice = Console.ReadLine();
                if (user.application.ContainsKey(choice))
                {
                    Console.Clear();
                    user.application[choice]();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That is an incorrect option entry, please try again.");
                    Console.Write("\n");
                    Console.Write("Press a letter: ");
                }
            }

        }



        public static string RoleMethod()
        {
            Console.Clear();
            bool CorrectRole = false;
            string role = "";
            while (CorrectRole == false)
            {
                CorrectRole = true;
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
                        Console.Clear();
                        Console.WriteLine("That is an incorrect option entry, please try again.");
                        Console.Write("\n");
                        CorrectRole = false;
                        break;
                }
            }
            Console.Clear();
            return role;
        }
    }
}
