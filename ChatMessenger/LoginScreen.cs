using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatMessenger
{
    public class LoginScreen
    {
        HelpMethods hm = new HelpMethods();
        DatabasesAccess da = new DatabasesAccess();
        

        public void LoginMethod()
        {
            bool foundPassword = false;
            string cmd = "select id, username, role, deleted from users";
            hm.LoginScreenMessage();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            while (foundPassword == false)
            {
                foreach (var u in users)
                {
                    if (username == u.username && u.deleted == false)
                    {
                        string TypeOfUser = u.role;
                        int Id = u.id;
                        hm.LoginScreenMessage();
                        foundPassword = PasswordMethod(username);
                        Console.Write("\n");
                        Console.Clear();
                        ApplicationsMenus.ApplicationMenu(username, TypeOfUser, Id);
                    }
                }
                if (foundPassword == false)
                {
                    hm.LoginScreenMessage();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect username");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                }
            }
        }




        public bool PasswordMethod(String username)
        {
            bool foundPassword = false;
            while (foundPassword == false)
            {
                string password = MaskMethod();
                Console.WriteLine("\n");
                string correct = da.ProcedureDatabases(username, password, "check_Password");
                if (correct != username)
                {
                    hm.LoginScreenMessage();
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




        public string MaskMethod()
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