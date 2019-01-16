using System;
using System.Collections.Generic;

namespace ChatMessenger
{

    public class ApplicationsMenus
    {
        public static int userId { get; set; }

        public delegate void menu();

        public delegate void EditUser();

        MainApplication ma = new MainApplication();
        HelpMethods hm = new HelpMethods();
        
        private Dictionary<string, string> roles = new Dictionary<string, string>()
        {
            {"a", "User"},
            {"b", "View Admin"},
            {"c", "View-Edit Admin"},
            {"d", "View-Edit-Delete Admin"},
            {"e", "Super Admin"}
        };



        public void ApplicationMenu(string username, string TypeOfUser, int Id)
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
            user = TypeOfUsers[TypeOfUser];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome {username}");
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
                    hm.IncorrectMessage();
                }
            }
        }



        public void ChatMenu()
        {
            Dictionary<string, menu> menuMessages = new Dictionary<string, menu>()
            {
                {"a", ma.SendMessage},
                {"b", ma.ViewMessage},
                {"c", ma.ViewNewMessage},
                {"d", ma.ViewAllMessageByUser}

            };


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
                hm.IncorrectMessage();
            }
        }


        public void UserMenu()
        {
            Dictionary<string, EditUser> editUser = new Dictionary<string, EditUser>()
            {
                {"a", ma.CreateUser},
                {"b", ma.ViewUser},
                {"c", ma.DeleteUser},
                {"d", ma.UpdateUserName},
                {"e", ma.UpdatePassword},
                {"f", ma.UpdateRole},
            };

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
                hm.IncorrectMessage();
            }
        }



        public string RoleMenu()
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
                    hm.IncorrectMessage();
                }
            }
        }


    }
}
