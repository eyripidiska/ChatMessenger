﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class HelpMethods
    {
        public static void SendMessage(int userId, int receiverId)
        {
            string message = Console.ReadLine();
            DatabasesAccess da = new DatabasesAccess();
            if (message.Length <= 250)
            {
                Console.Clear();
                da.InsertMessagesDatabase(userId, receiverId, message);
                FindUserName(userId, receiverId, message);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("the message is over to 250 characters");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n");
            }
        }





        public static void FindUserName(int SenderId, int receiverId, string message)
        {
            string Sender;
            string receiver;
            string cmd = "select * from users where deleted = 0";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = da.MessagesDatabase(cmd);
            Sender = ReturnUsernameFromId(users, SenderId);
            receiver = ReturnUsernameFromId(users, receiverId);
            FilesAccess.Files(Sender, receiver, message, SenderId, receiverId);
        }


        public static string SamePassword()
        {
            bool samePassword = false;
            string password1 = "";
            string password2 = "";
            while (samePassword == false)
            {
                Console.Write("Type the ");
                password1 = LoginScreen.Mask();
                Console.Write("\n");
                Console.Write("Repeat the ");
                password2 = LoginScreen.Mask();
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


        public static int ReturnIdFromUsername(IEnumerable<User> users, string username)
        {
            var Id = users
                    .Where(x => x.username == username)
                    .Select(x => x.id);
            int id = Convert.ToInt32(Id.FirstOrDefault());
            return id;
        }



        public static string ReturnUsernameFromId(IEnumerable<User> users, int id)
        {
            var username = users
                    .Where(x => id == x.id)
                    .Select(x => x.username)
                    .FirstOrDefault();
            return username;
        }



        public static bool CheckNoExistUser(string username)
        {
            string cmd = "select * from users";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
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

        public static bool CheckExistUser(IEnumerable<User> users, string username)
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


        public static bool CheckUser(IEnumerable<dynamic> users, string username)
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



        public static bool CheckExistUser(string username)
        {
            string cmd = "select * from users";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);

            bool UserExist = users
                .Any(x => x.username == username && x.deleted == false);

            return UserExist;
        }



        public static bool CheckExistMessage(int id)
        {
            string cmd = "SELECT * FROM messages";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> messages = da.UsersDatabase(cmd);
            bool MessageExist = messages
                .Any(x => x.id == id && x.deleted == false);
            return MessageExist;
        }

        public static void LoginScreenMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Login Screen");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }
        public static void IncorrectMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That is an incorrect option entry, please try again.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }




        public static void UserDoesNotExistMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The user does not exist");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }

        public static void ReturnBackMessage()
        {
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
            Console.Clear();
        }


        public static void MessageDoesNotExist()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The message does not exist");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }

        public static void MessageErrorConnection()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The connection with the database was unsuccessful");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
            Console.ReadKey();
            Console.Clear();
            LoginScreen.Login();
        }
    }
}
