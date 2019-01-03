using System;
using System.Collections.Generic;

namespace ChatMessenger
{
    class MainApplication
    {
        public static void CreateUserMethod()
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            string username = LoginScreen.CheckExistUser(users);
            string password = LoginScreen.SamePasswordMethod();
            string role = ApplicationsMenus.RoleMethod();
            DatabasesAccess.InsertUsers(username, password, role);
        }



        public static void ViewUserMethod()
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine("Username: " + u.username + " - " + "Role: " +  u.role);
            }
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
        }



        public static void DeleteMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == false)
            {
                Console.Write("The user does not exist");
                Console.WriteLine("\n");
            }
            DatabasesAccess.DeleteUsers(username);
        }



        public static void UpdatePasswordMethod()
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



        public static void UpdateRoleMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ConnectDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.WriteLine("------------------------------------------------------");
                string role = ApplicationsMenus.RoleMethod();
                DatabasesAccess.UpdateRole(username, role);
            }
            else
            {
                Console.Write("The user does not exist");
            }
        }
    }
}
