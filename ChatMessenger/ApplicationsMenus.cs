﻿using System;
using System.Collections.Generic;

namespace ChatMessenger
{

    class ApplicationsMenus
    {
        public static int userId { get; set; }

        public delegate void menu();

        public delegate void EditUser();

        private static Dictionary<string, string> roles = new Dictionary<string, string>()
        {
            {"a", "User"},
            {"b", "View Admin"},
            {"c", "View-Edit Admin"},
            {"d", "View-Edit-Delete Admin"},
            {"e", "Super Admin"}
        };



        private static Dictionary<string, menu> menuMessages = new Dictionary<string, menu>()
        {
            {"a", MainApplication.SendMessage},
            {"b", MainApplication.ViewMessage},
            {"c", MainApplication.ViewNewMessage},
            {"d", MainApplication.ViewAllMessageByUser}

        };

        public static Dictionary<string, EditUser> editUser = new Dictionary<string, EditUser>()
        {
            {"a", MainApplication.CreateUser},
            {"b", MainApplication.ViewUser},
            {"c", MainApplication.DeleteUser},
            {"d", MainApplication.UpdateUserName},
            {"e", MainApplication.UpdatePassword},
            {"f", MainApplication.UpdateRole},
        };


        public static void ApplicationMenu(User myUser)
        {
            Users user;
            Dictionary<string, Users> TypeOfUsers = new Dictionary<string, Users>()
            {
                {"User", new User(myUser.username, myUser.id)},
                {"View Admin", new ViewAdmin(myUser.username, myUser.id)},
                {"View-Edit Admin", new ViewEditAdmin(myUser.username, myUser.id)},
                {"View-Edit-Delete Admin", new ViewEditDeleteAdmin(myUser.username, myUser.id)},
                {"Super Admin", new SuperAdmin(myUser.username, myUser.id)}
            };
            user = TypeOfUsers[myUser.role];
            userId = user.id;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome {myUser.username}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
            while (true)
            {
                user.PublicMenu();
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter an option: ");
                Console.ForegroundColor = ConsoleColor.White;
                string choice = Console.ReadLine();
                if (user.application.ContainsKey(choice))
                {
                    Console.Clear();
                    user.application[choice]();
                }
                else
                {
                    HelpMethods.IncorrectMessage();
                }
            }
        }



        public static void ChatMenu()
        {
            Console.WriteLine("CHAT MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("a. Send Message");
            Console.WriteLine("b. Read Messages");
            Console.WriteLine("c. Read New Messages");
            Console.WriteLine("d. Read All Messages");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter an option: ");
            Console.ForegroundColor = ConsoleColor.White;
            string choice = Console.ReadLine();

            if (menuMessages.ContainsKey(choice))
            {
                Console.Clear();
                menuMessages[choice]();
            }
            else
            {
                HelpMethods.IncorrectMessage();
            }
        }


        public static void UserMenu()
        {
            Console.WriteLine("USER MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("a. Create new User");
            Console.WriteLine("b. View Users");
            Console.WriteLine("c. Delete Users");
            Console.WriteLine("d. Update Username ");
            Console.WriteLine("e. Update Password");
            Console.WriteLine("f. Update Role");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter an option: ");
            Console.ForegroundColor = ConsoleColor.White;
            string choice = Console.ReadLine();
            if (editUser.ContainsKey(choice))
            {
                Console.Clear();
                editUser[choice]();
            }
            else
            {
                HelpMethods.IncorrectMessage();
            }
        }



        public static string RoleMenu()
        {
            while (true)
            {
                Console.WriteLine("ROLES MENU - Please choose an option.");
                Console.WriteLine("=====================================");
                Console.WriteLine("a. Set User");
                Console.WriteLine("b. Set Admin with View Privilege");
                Console.WriteLine("c. Set Admin with View and Edit Privilege");
                Console.WriteLine("d. Set Admin with View, Edit and Delete Privilege");
                Console.WriteLine("e. Set Super Admin");
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter an option: ");
                Console.ForegroundColor = ConsoleColor.White;
                string key = Console.ReadLine();

                if (roles.ContainsKey(key))
                {
                    Console.Clear();
                    return roles[key];
                }
                else
                {
                    HelpMethods.IncorrectMessage();
                }
            }
        }


    }
}
