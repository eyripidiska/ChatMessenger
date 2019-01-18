using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class LoginScreen
    {
        public static void Login()
        {
            bool foundPassword = false;
            string cmd = "select id, username, role, deleted from users";
            DatabasesAccess da = new DatabasesAccess();
            HelpMethods.LoginScreenMessage();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            while (foundPassword == false)
            {
                var myUser = users.FirstOrDefault(u => u.username == username && !u.deleted);
                if (myUser != null && Password(username))
                {
                    HelpMethods.LoginScreenMessage();
                    Console.Write("\n");
                    Console.Clear();
                    ApplicationsMenus.ApplicationMenu(myUser);
                }
                else
                {
                    HelpMethods.LoginScreenMessage();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect username");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                }
            }
        }




        public static bool Password(String username)
        {
            bool foundPassword = false;
            DatabasesAccess da = new DatabasesAccess();
            while (foundPassword == false)
            {
                string password = Mask();
                Console.WriteLine("\n");
                string correct = da.ProcedureDatabases(username, password, "check_Password");
                if (correct != username)
                {
                    HelpMethods.LoginScreenMessage();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect password try again");
                    Console.ForegroundColor = ConsoleColor.White;
                    password = "";
                }
                else
                {
                    foundPassword = true;
                }
            }
            return foundPassword;
        }




        public static string Mask()
        {
            string password = "";
            Console.Write("Password: ");
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            return password;
        }
    }
}