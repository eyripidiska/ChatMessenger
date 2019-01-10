using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class MainApplication
    {
        public static void SendMessage()
        {
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.Write("Type the user you want to exchange messages: ");
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
                Message.SendMessageMethod(userId, ReceiverId);
            }
            else
            {
                HelpMethods.UserDoesNotExistMessageMethod();
            }
        }


        public static void ViewMessage()
        {
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.Write("Type the user you want to read the messages: ");
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
                cmd = "SELECT * FROM messages WHERE((senderId = @senderId AND receiverId = @receiverId) OR (senderId = @receiverId AND receiverId = @senderId)) AND deleted = 0;";
                IEnumerable<dynamic> messages = DatabasesAccess.ReadMessagesDatabase(cmd, userId, ReceiverId);
                foreach (var m in messages)
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
                HelpMethods.ReturnBackMessageMethod();
            }
        }


        public static void ViewAllMessageByUserMethod()
        {
            Console.Clear();
            int SenderId;
            string Sender;
            int userId = ApplicationsMenus.userId;
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE receiverId = @receiverId AND deleted = 0;";
            IEnumerable<dynamic> messages = DatabasesAccess.ReadMessagesDatabase(cmd, userId);
            foreach (var m in messages)
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
            HelpMethods.ReturnBackMessageMethod();
        }


        public static void ViewAllMessageMethod()
        {
            Console.Clear();
            int SenderId;
            int receiverId;
            string Sender;
            string receiver;
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<dynamic> messages = DatabasesAccess.ReturnQueryDatabase(cmd);
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
            HelpMethods.ReturnBackMessageMethod();
        }


        public static void EditMessageMethod()
        {
            string cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<dynamic> messages = DatabasesAccess.ReturnQueryDatabase(cmd);
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
                    Message.SendMessageMethod(SenderId, ReceiverId);
                }
                else
                {
                    HelpMethods.MessageDoesNotExistMethod();
                }
            }
            else
            {
                HelpMethods.IncorrectMessageMethod();
            }
        }

        public static void DeleteMessageMethod()
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
                    HelpMethods.MessageDoesNotExistMethod();
                }
            }
            else
            {
                HelpMethods.IncorrectMessageMethod();
            }

        }


        public static void CreateUserMethod()
        {
            Console.Write("Type the username: ");
            string username = Console.ReadLine();
            bool userExist = HelpMethods.CheckNoExistUser(username);
            if (userExist == false)
            {
                string password = HelpMethods.SamePasswordMethod();
                Console.Clear();
                string role = ApplicationsMenus.RoleMenuMethod();
                DatabasesAccess.InsertUsersDatabase(username, password, role);
            }
        }



        public static void ViewUserMethod()
        {
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine("Username: " + u.username + " - " + "Role: " + u.role);
            }
            HelpMethods.ReturnBackMessageMethod();
        }



        public static void DeleteUserMethod()
        {
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                DatabasesAccess.DeleteUsersDatabase(username);
            }
            else
            {
                HelpMethods.UserDoesNotExistMessageMethod();
            }
        }


        public static void UpdateUserNameMethod()
        {
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
                    DatabasesAccess.UpdateUserNameDatabase(username, newUsername);
                }
            }
            else
            {
                HelpMethods.UserDoesNotExistMessageMethod();
            }
        }


        public static void UpdatePasswordMethod()
        {
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                string password = HelpMethods.SamePasswordMethod();
                Console.Clear();
                DatabasesAccess.UpdatePasswordDatabase(username, password);
            }
            else
            {
                HelpMethods.UserDoesNotExistMessageMethod();
            }
        }



        public static void UpdateRoleMethod()
        {
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool UserExist = HelpMethods.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                string role = ApplicationsMenus.RoleMenuMethod();
                DatabasesAccess.UpdateRoleDatabase(username, role);
            }
            else
            {
                HelpMethods.UserDoesNotExistMessageMethod();
            }
        }
    }
}
