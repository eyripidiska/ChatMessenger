using System;
using System.Collections.Generic;

namespace ChatMessenger
{

    class ApplicationsMenus
    {
        public static int userId { get; set; }

        private static Dictionary<string, string> roles = new Dictionary<string, string>()
        {
            {"a", "User"},
            {"b", "View Admin"},
            {"c", "View-Edit Admin"},
            {"d", "View-Edit-Delete Admin"},
            {"e", "Super Admin"}
        };

        public delegate void menu();

        private static Dictionary<string, menu> menuMessages = new Dictionary<string, menu>()
        {
            {"a", MainApplication.SendMessage},
            {"b", MainApplication.ViewMessage},
            {"c", MainApplication.ViewNewMessage}
        };



        public static void ApplicationMenuMethod(string username, string TypeOfUser, int Id)
        {
            Users user;
            userId = Id;
            Dictionary<string, Users> TypeOfUsers = new Dictionary<string, Users>()
            {
                {"User", new User(username, Id)},
                {"View Admin", new ViewAdmin(username, Id)},
                {"View-Edit Admin", new ViewEditAdmin(username, Id)},
                {"View-Edit-Delete Admin", new ViewEditDeleteAdmin(username, Id)},
                {"Super Admin", new SuperAdmin(username, Id)}
            };
            Console.Clear();
            user = TypeOfUsers[TypeOfUser];
            Console.WriteLine($"Welcome {username}");
            Console.Write("\n");
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
                }
            }
        }



        public static void MessageMenuMethod()
        {
            Console.WriteLine("To send a message to a user press {a}");
            Console.WriteLine("To read a message from a user press {b}");
            Console.WriteLine("To read the new messages press {c}");
            string choice = Console.ReadLine();

            if (menuMessages.ContainsKey(choice))
            {
                Console.Clear();
                menuMessages[choice]();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("That is an incorrect option entry, please try again.");
                Console.Write("\n");
            }
        }




        public static string RoleMenuMethod()
        {
            while (true)
            {
                Console.WriteLine("To set user privilege press {a}");
                Console.WriteLine("To set Admin with view privilege press {b}");
                Console.WriteLine("To set Admin with view and edit privilege press {c}");
                Console.WriteLine("To set Admin with view, Edit and Delete privilege press {d}");
                Console.WriteLine("To set super Admin privilege press {e}");
                Console.Write("\n");
                Console.Write("Press a letter: ");
                string key = Console.ReadLine();

                if (roles.ContainsKey(key))
                {
                    Console.Clear();
                    return roles[key];
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That is an incorrect option entry, please try again.");
                    Console.Write("\n");
                }
            }
        }
    }
}
