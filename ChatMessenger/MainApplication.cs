using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class MainApplication
    {
        public static void SendMessage()
        {
            string cmd = "select id, username from users where deleted = 0";
            IEnumerable<User> users = DatabasesAccess.ReturnUsersDatabase(cmd);
            Console.Write("Type the username you want to exchange messages: ");
            string Username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(users, Username);
            if (UserExist == true)
            {
                var receiverId = users
                    .Where(x => x.username == Username)
                    .Select(x => x.id);
                int ReceiverId = Convert.ToInt32(receiverId.FirstOrDefault());
                int userId = ApplicationsMenus.userId;
                Console.Clear();
                Console.WriteLine($"Write a message to {Username}, the maximun text limited to 250 characters");
                Console.WriteLine("\n");
                Message.SendMessage(userId, ReceiverId);
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }


        public static void ViewMessage()
        {
            string cmd = "select id, username from users";
            IEnumerable<User> users = DatabasesAccess.ReturnUsersDatabase(cmd);
            Console.Write("Type the username you want to read the messages: ");
            string Username = Console.ReadLine();
            bool existUser = HelpMethods.CheckExistUser(users, Username);
            if (existUser == true)
            {
                var receiverId = users
                    .Where(x => x.username == Username)
                    .Select(x => x.id);
                int ReceiverId = Convert.ToInt32(receiverId.FirstOrDefault());
                int userId = ApplicationsMenus.userId;
                Console.Clear();
                cmd = "SELECT * FROM messages WHERE deleted = 0;";
                IEnumerable<Message> messages = DatabasesAccess.ReturnMessagesDatabase(cmd);
                var myMessages = messages.Where(m => (m.receiverId == ReceiverId && m.senderId == userId) || (m.senderId == ReceiverId && m.receiverId == userId));

                foreach (var m in myMessages)
                {
                    if (m.senderId == userId)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("You: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(m.messageData);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Username + ": ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(m.messageData);
                    }
                }
                HelpMethods.ReturnBackMessage();
            }
        }


        public static void ViewAllMessageByUser()
        {
            Console.Clear();
            int SenderId;
            string Sender;
            int userId = ApplicationsMenus.userId;
            string cmd = "select id, username from users";
            IEnumerable<User> users = DatabasesAccess.ReturnUsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0;";
            IEnumerable<Message> messages = DatabasesAccess.ReturnMessagesDatabase(cmd);
            var myMessage = messages.Where(m => m.receiverId == userId).ToList();
            foreach (var m in myMessage)
            {

                SenderId = m.senderId;

                Sender = users
                    .Where(x => SenderId == x.id)
                    .Select(x => x.username)
                    .FirstOrDefault();

                Console.Write("From: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Sender);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" - Message: " + m.messageData);

            }
            HelpMethods.ReturnBackMessage();
        }


        public static void ViewAllMessage()
        {
            Console.Clear();
            int SenderId;
            int receiverId;
            string Sender;
            string receiver;
            string cmd = "select id, username from users";
            IEnumerable<User> users = DatabasesAccess.ReturnUsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = DatabasesAccess.ReturnMessagesDatabase(cmd);
            foreach (var m in messages)
            {
                SenderId = m.senderId;
                receiverId = m.receiverId;

                Sender = users
                    .Where(x => SenderId == x.id)
                    .Select(x => x.username)
                    .FirstOrDefault();

                receiver = users
                    .Where(x => receiverId == x.id)
                    .Select(x => x.username)
                    .FirstOrDefault();

                Console.Write("Id: " + m.id + " - From: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Sender);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" - To: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(receiver);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" - Message: " + m.messageData);

            }
            HelpMethods.ReturnBackMessage();
        }


        public static void EditMessage()
        {
            string cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = DatabasesAccess.ReturnMessagesDatabase(cmd);
            Console.Write("Type the id of message you want to update: ");

            int id;
            string ID = Console.ReadLine();
            bool result = int.TryParse(ID, out id);
            if (result)
            {
                bool MessageExist = HelpMethods.CheckExistMessage(id);
                if (MessageExist == true)
                {
                    var senderId = messages
                          .Where(x => x.id == id)
                          .Select(x => x.senderId);
                    int SenderId = Convert.ToInt32(senderId.FirstOrDefault());

                    var receiverId = messages
                        .Where(x => x.id == id)
                        .Select(x => x.receiverId);
                    int ReceiverId = Convert.ToInt32(receiverId.FirstOrDefault());
                    DatabasesAccess.DeleteMessagesDatabase(id);
                    Console.Clear();
                    Console.WriteLine($"Write the new message the maximun text limited to 250 characters");
                    Console.WriteLine("\n");
                    Message.SendMessage(SenderId, ReceiverId);
                }
                else
                {
                    HelpMethods.MessageDoesNotExist();
                }
            }
            else
            {
                HelpMethods.IncorrectMessage();
            }
        }

        public static void DeleteMessage()
        {
            Console.Write("Type the id of message you want to delete: ");

            int id;
            string ID = Console.ReadLine();
            bool result = int.TryParse(ID, out id);
            if (result)
            {
                bool MessageExist = HelpMethods.CheckExistMessage(id);
                if (MessageExist == true)
                {
                    Console.Clear();
                    DatabasesAccess.DeleteMessagesDatabase(id);
                }
                else
                {
                    HelpMethods.MessageDoesNotExist();
                }
            }
            else
            {
                HelpMethods.IncorrectMessage();
            }

        }


        public static void CreateUser()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username: ");
            string username = Console.ReadLine();
            bool userExist = HelpMethods.CheckNoExistUser(username);
            if (userExist == false)
            {
                string password = HelpMethods.SamePassword();
                Console.Clear();
                string role = ApplicationsMenus.RoleMenu();
                DBDictionary.Add("@username", username);
                DBDictionary.Add("@pass", password);
                DBDictionary.Add("@role", role);
                DatabasesAccess.ProcedureDatabase(DBDictionary, "Insert_Users");
            }
        }



        public static void ViewUser()
        {
            string cmd = "select username, role from users where deleted = 0";
            IEnumerable<User> users = DatabasesAccess.ReturnUsersDatabase(cmd);
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine("Username: " + u.username + " - " + "Role: " + u.role);
            }
            HelpMethods.ReturnBackMessage();
        }



        public static void DeleteUser()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                DBDictionary.Add("@username", username);
                DatabasesAccess.ProcedureDatabase(DBDictionary, "Delete_Users");
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }


        public static void UpdateUserName()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool userNotExist;
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                Console.Write("Type a new username: ");
                string newUsername = Console.ReadLine();
                userNotExist = HelpMethods.CheckNoExistUser(newUsername);
                if (userNotExist == false)
                {
                    Console.Clear();
                    DBDictionary.Add("username", username);
                    DBDictionary.Add("newUsername", newUsername);
                    DatabasesAccess.ProcedureDatabase(DBDictionary, "Update_UserName");
                }
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }


        public static void UpdatePassword()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                string password = HelpMethods.SamePassword();
                Console.Clear();
                DBDictionary.Add("username", username);
                DBDictionary.Add("newPassword", password);
                DatabasesAccess.ProcedureDatabase(DBDictionary, "Update_Users_By_Password");
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }



        public static void UpdateRole()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                string role = ApplicationsMenus.RoleMenu();
                DBDictionary.Add("username", username);
                DBDictionary.Add("newRole", role);
                DatabasesAccess.ProcedureDatabase(DBDictionary, "Update_Users_By_Role");
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }
    }
}
