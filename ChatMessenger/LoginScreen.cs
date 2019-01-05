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
                    if (username == u.username && u.deleted == true)
                    {
                        string Password = u.pass;
                        int salt = u.salt;
                        string TypeOfUser = u.role;
                        Console.Clear();
                        Console.WriteLine("Login Screen");
                        Console.Write("\n");
                        foundPassword = PasswordMethod(Password, salt);
                        Console.Write("\n");
                        Console.Clear();
                        ApplicationsMenus.ApplicationMethod(username, TypeOfUser);
                    }
                }
                if (foundPassword == false)
                {
                    Console.Clear();
                    Console.WriteLine("Login Screen");
                    Console.Write("\n");
                    Console.WriteLine("Incorrect username");
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

                // step 1, calculate MD5 hash from input
                MD5 md5 = System.Security.Cryptography.MD5.Create();

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(encryptedPassword);

                byte[] hash = md5.ComputeHash(inputBytes);

                // step 2, convert byte array to hex string
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
                    Console.WriteLine("Incorrect password try again");
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
                // Skip if Backspace or Enter is Pressed
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Remove last charcter if Backspace is Pressed
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            return password;
        }





        public static string SamePasswordMethod()
        {
            bool samePassword = false;
            string password1 = "";
            string password2 = "";
            while (samePassword == false)
            {
                Console.Write("Type the ");
                password1 = MaskMethod();
                Console.Write("\n");
                Console.Write("Repeat the ");
                password2 = MaskMethod();
                if (password1 == password2)
                {
                    samePassword = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("the passwords doesn't match");
                }
            }
            Console.Write("\n");
            return password1;
        }



        public static string CheckExistUser(IEnumerable<dynamic> users)
        {
            bool UserExist;
            Console.Write("Type the username: ");
            string username = Console.ReadLine();
            do
            {
                UserExist = false;
                foreach (var u in users)
                {
                    if (username == u.username && u.deleted == true)
                    {
                        Console.Clear();
                        Console.Write("The user exist type another username: ");
                        username = Console.ReadLine();
                        UserExist = true;
                    }
                }
            }
            while (UserExist == true);
            return username;
        }

        public static bool ReturnExistUser(IEnumerable<dynamic> users, string username)
        {
            bool UserExist = false;
            foreach (var u in users)
            {
                if (username == u.username && u.deleted == true)
                {
                    UserExist = true;
                }
            }
            if (UserExist == false)
            {
                Console.Write("The user does not exist");
            }
            return UserExist;
        }



        public static bool CheckExistUser(IEnumerable<dynamic> users, string username)
        {
            bool UserExist = false;
            foreach (var u in users)
            {
                if (username == u.username && u.deleted == true)
                {
                    UserExist = true;
                }
            }
            return UserExist;
        }

    }
}