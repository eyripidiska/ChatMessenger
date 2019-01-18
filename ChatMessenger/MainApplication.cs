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
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            Console.Write("Type the username you want to exchange messages: ");
            string Username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(users, Username);
            if (UserExist == true)
            {
                int ReceiverId = HelpMethods.ReturnIdFromUsername(users, Username);
                int userId = ApplicationsMenus.userId;
                Console.Clear();
                Console.Write($"Write a message to ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Username);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", the maximun text limited to 250 characters");
                Console.WriteLine("\n");
                HelpMethods.SendMessage(userId, ReceiverId);
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }


        public static void ViewMessage()
        {
            string cmd = "select id, username from users";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            Console.Write("Type the username you want to read the messages: ");
            string Username = Console.ReadLine();
            bool existUser = HelpMethods.CheckExistUser(users, Username);
            if (existUser == true)
            {
                int ReceiverId = HelpMethods.ReturnIdFromUsername(users, Username);
                int userId = ApplicationsMenus.userId;
                Console.Clear();
                cmd = "SELECT * FROM messages WHERE deleted = 0;";
                IEnumerable<Message> messages = da.MessagesDatabase(cmd);
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


        public static void ViewNewMessage()
        {
            Console.Clear();
            int SenderId;
            int userId = ApplicationsMenus.userId;
            string cmd = "select id, username from users";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0 AND readed = 0;";
            IEnumerable<Message> messages = da.MessagesDatabase(cmd);
            var myMessage = messages.Where(m => m.receiverId == userId).ToList();
            foreach (var m in myMessage)
            {
                SenderId = m.senderId;
                string Sender = HelpMethods.ReturnUsernameFromId(users, SenderId);

                Console.Write("From: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Sender);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" - Message: " + m.messageData);
                da.ProcessMessagesDatabase(m.id, "Read_messages");
            }
            HelpMethods.ReturnBackMessage();
        }



        public static void ViewAllMessageByUser()
        {
            Console.Clear();
            int SenderId;
            int userId = ApplicationsMenus.userId;
            string cmd = "select id, username from users";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0;";
            IEnumerable<Message> messages = da.MessagesDatabase(cmd);
            var myMessage = messages.Where(m => m.receiverId == userId).ToList();
            foreach (var m in myMessage)
            {

                SenderId = m.senderId;
                string Sender = HelpMethods.ReturnUsernameFromId(users, SenderId);

                Console.Write("From: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Sender);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" - Message: " + m.messageData);
                da.ProcessMessagesDatabase(m.id, "Read_messages");
                Console.Write("\n");
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
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = da.MessagesDatabase(cmd);
            foreach (var m in messages)
            {
                SenderId = m.senderId;
                receiverId = m.receiverId;
                Sender = HelpMethods.ReturnUsernameFromId(users, SenderId);
                receiver = HelpMethods.ReturnUsernameFromId(users, receiverId);

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
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<Message> messages = da.MessagesDatabase(cmd);
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
                    da.ProcessMessagesDatabase(id, "Delete_messages");
                    Console.Clear();
                    Console.WriteLine($"Write the new message the maximun text limited to 250 characters");
                    Console.WriteLine("\n");
                    HelpMethods.SendMessage(SenderId, ReceiverId);
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
            DatabasesAccess da = new DatabasesAccess();
            if (result)
            {
                bool MessageExist = HelpMethods.CheckExistMessage(id);
                if (MessageExist == true)
                {
                    Console.Clear();
                    da.ProcessMessagesDatabase(id, "Delete_messages");
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
            DatabasesAccess da = new DatabasesAccess();
            if (userExist == false)
            {
                string password = HelpMethods.SamePassword();
                Console.Clear();
                string role = ApplicationsMenus.RoleMenu();
                DBDictionary.Add("@username", username);
                DBDictionary.Add("@pass", password);
                DBDictionary.Add("@role", role);
                da.ProcedureDatabase(DBDictionary, "Insert_Users");
            }
        }



        public static void ViewUser()
        {
            string cmd = "select username, role from users where deleted = 0";
            DatabasesAccess da = new DatabasesAccess();
            IEnumerable<User> users = da.UsersDatabase(cmd);
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
            DatabasesAccess da = new DatabasesAccess();
            if (UserExist == true)
            {
                Console.Clear();
                DBDictionary.Add("@username", username);
                da.ProcedureDatabase(DBDictionary, "Delete_Users");
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
            DatabasesAccess da = new DatabasesAccess();
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
                    da.ProcedureDatabase(DBDictionary, "Update_UserName");
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
            DatabasesAccess da = new DatabasesAccess();
            if (UserExist == true)
            {
                Console.Clear();
                string password = HelpMethods.SamePassword();
                Console.Clear();
                DBDictionary.Add("username", username);
                DBDictionary.Add("newPassword", password);
                da.ProcedureDatabase(DBDictionary, "Update_Users_By_Password");
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
            DatabasesAccess da = new DatabasesAccess();
            if (UserExist == true)
            {
                Console.Clear();
                string role = ApplicationsMenus.RoleMenu();
                DBDictionary.Add("username", username);
                DBDictionary.Add("newRole", role);
                da.ProcedureDatabase(DBDictionary, "Update_Users_By_Role");
            }
            else
            {
                HelpMethods.UserDoesNotExistMessage();
            }
        }
    }
}
