using System;
using System.Collections.Generic;

namespace ChatMessenger
{
    class MainApplication
    {
        public static void MessageMethod()
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.Write("Type the user you want to exchange messages: ");
            string username = Console.ReadLine();
            bool existUser = LoginScreen.ReturnExistUser(users, username);
            if (existUser == true)
            {
                Console.WriteLine("evrika");
            }
        }


        public static void ViewMessageMethod()
        {

        }


        public static void EditMessageMethod()
        {

        }

        public static void DeleteMessageMethod()
        {

        }


        public static void CreateUserMethod()
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            string username = LoginScreen.CheckExistUser(users);
            string password = LoginScreen.SamePasswordMethod();
            string role = ApplicationsMenus.RoleMethod();
            DatabasesAccess.InsertUsersDatabase(username, password, role);
        }



        public static void ViewUserMethod()
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine("Username: " + u.username + " - " + "Role: " +  u.role);
            }
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
            Console.Clear();
        }



        public static void DeleteUserMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == false)
            {
                Console.Clear();
                Console.Write("The user does not exist");
                Console.WriteLine("\n");
            }
            Console.Clear();
            DatabasesAccess.DeleteUsersDatabase(username);
        }



        public static void UpdatePasswordMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.Clear();
                string password = LoginScreen.SamePasswordMethod();
                Console.Clear();
                DatabasesAccess.UpdatePasswordDatabase(username, password);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("The user does not exist");
                Console.Write("\n");
            }
        }



        public static void UpdateRoleMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = LoginScreen.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.Clear();
                string role = ApplicationsMenus.RoleMethod();
                DatabasesAccess.UpdateRoleDatabase(username, role);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("The user does not exist");
                Console.Write("\n");
            }
        }
    }
}
