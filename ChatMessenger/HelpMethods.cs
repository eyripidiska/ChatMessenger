using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class HelpMethods
    {
        DatabasesAccess da = new DatabasesAccess();
        LoginScreen ls = new LoginScreen();
        public string SamePassword()
        {
            bool samePassword = false;
            string password1 = "";
            string password2 = "";
            while (samePassword == false)
            {
                Console.Write("Type the ");
                password1 = ls.MaskMethod();
                Console.Write("\n");
                Console.Write("Repeat the ");
                password2 = ls.MaskMethod();
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


        public int ReturnIdFromUsername(IEnumerable<User> users, string username)
        {
            var Id = users
                    .Where(x => x.username == username)
                    .Select(x => x.id);
            int id = Convert.ToInt32(Id.FirstOrDefault());
            return id;
        }



        public string ReturnUsernameFromId(IEnumerable<User> users, int id)
        {
            var username = users
                    .Where(x => id == x.id)
                    .Select(x => x.username)
                    .FirstOrDefault();
            return username;
        }



        public bool CheckNoExistUser(string username)
        {
            string cmd = "select * from users";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            bool UserExist = users
                .Any(x => x.username == username && x.deleted == false);

            if (UserExist == true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("The user exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n");
            }
            return UserExist;
        }

        public bool CheckExistUser(IEnumerable<User> users, string username)
        {
            bool UserExist = false;

            UserExist = users
                .Any(x => x.username == username && x.deleted == false);

            if (UserExist == false)
            {
                UserDoesNotExistMessage();
            }
            return UserExist;
        }


        public bool CheckUser(IEnumerable<dynamic> users, string username)
        {
            bool UserExist = false;

            UserExist = users
                .Any(x => x.username == username);

            if (UserExist == false)
            {
                UserDoesNotExistMessage();
            }
            return UserExist;
        }



        public bool CheckExistUser(string username)
        {
            string cmd = "select * from users";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);

            bool UserExist = users
                .Any(x => x.username == username && x.deleted == false);

            return UserExist;
        }



        public bool CheckExistMessage(int id)
        {
            string cmd = "SELECT * FROM messages";
            IEnumerable<User> messages = da.ReturnUsersDatabase(cmd);
            bool MessageExist = messages
                .Any(x => x.id == id && x.deleted == false);
            return MessageExist;
        }

        public void LoginScreenMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Login Screen");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }
        public void IncorrectMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That is an incorrect option entry, please try again.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }




        public void UserDoesNotExistMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The user does not exist");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }

        public void ReturnBackMessage()
        {
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
            Console.Clear();
        }


        public void MessageDoesNotExist()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The message does not exist");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }

        public void MessageErrorConnection()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The connection with the database was unsuccessful");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
            Console.ReadKey();
            Console.Clear();
            ls.LoginMethod();
        }
    }
}
