using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatMessenger
{
    class LoginScreen
    {
        public static void LoginMethod()
        {
            bool foundPassword = false;
            string cmd = "select * from users";
            Console.WriteLine("Login Screen");
            Console.WriteLine("\n");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            while (foundPassword == false)
            {
                foreach (var u in users)
                {
                    if (username == u.username && u.deleted == false)
                    {
                        string Password = u.pass;
                        int salt = u.salt;
                        string TypeOfUser = u.role;
                        int Id = u.id;
                        Console.Clear();
                        Console.WriteLine("Login Screen");
                        Console.Write("\n");
                        foundPassword = PasswordMethod(Password, salt);
                        Console.Write("\n");
                        Console.Clear();
                        ApplicationsMenus.ApplicationMenuMethod(username, TypeOfUser, Id);
                    }
                }
                if (foundPassword == false)
                {
                    Console.Clear();
                    Console.WriteLine("Login Screen");
                    Console.Write("\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect username");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                }
            }
        }




        public static bool PasswordMethod(string Password, int salt)
        {
            bool foundPassword = false;
            while (foundPassword == false)
            {
                string password = MaskMethod();
                Console.WriteLine("\n");
                string encryptedPassword = salt.ToString() + password;
                
                MD5 md5 = System.Security.Cryptography.MD5.Create();

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(encryptedPassword);

                byte[] hash = md5.ComputeHash(inputBytes);
                
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                encryptedPassword = sb.ToString();

                if (encryptedPassword != Password)
                {
                    Console.Clear();
                    Console.WriteLine("Login Screen");
                    Console.Write("\n");
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




        public static string MaskMethod()
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