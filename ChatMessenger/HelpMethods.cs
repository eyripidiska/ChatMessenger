using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class HelpMethods
    {
        public static string SamePasswordMethod()
        {
            bool samePassword = false;
            string password1 = "";
            string password2 = "";
            while (samePassword == false)
            {
                Console.Write("Type the ");
                password1 = LoginScreen.MaskMethod();
                Console.Write("\n");
                Console.Write("Repeat the ");
                password2 = LoginScreen.MaskMethod();
                if (password1 == password2)
                {
                    samePassword = true;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("the passwords doesn't match");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.Write("\n");
            return password1;
        }



        public static string ReturnNoExistUser(IEnumerable<dynamic> users)
        {
            bool UserExist;
            Console.Write("Type the username: ");
            string username = Console.ReadLine();
            do
            {
                UserExist = users
               .Any(x => x.username == username && x.deleted == false);
                
                if (UserExist == true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The user exist type another username: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    username = Console.ReadLine();
                    UserExist = true;
                }
            }
            while (UserExist == true);
            return username;
        }

        public static bool ReturnExistUser(IEnumerable<dynamic> users, string username)
        {
            bool UserExist = false;

            UserExist = users
                .Any(x => x.username == username && x.deleted == false);

            if (UserExist == false)
            {
                UserDoesNotExistMessageMethod();
            }
            return UserExist;
        }



        public static bool CheckExistUser(string username)
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = false;

            UserExist = users
                .Any(x => x.username == username && x.deleted == false);

            return UserExist;
        }




        public static void IncorrectMessageMethod()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That is an incorrect option entry, please try again.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }




        public static void UserDoesNotExistMessageMethod()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The user does not exist");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }

        public static void ReturnBackMessageMethod()
        {
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
            Console.Clear();
        }



    }
}
