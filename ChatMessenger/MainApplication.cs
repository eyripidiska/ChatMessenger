using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    class MainApplication
    {

        DatabasesAccess da = new DatabasesAccess();
        HelpMethods hm = new HelpMethods();
        ApplicationsMenus am = new ApplicationsMenus();
        Message me = new Message();

        public void SendMessage()
        {
            string cmd = "select id, username from users where deleted = 0";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            Console.Write("Type the username you want to exchange messages: ");
            string Username = Console.ReadLine();
            bool UserExist = hm.CheckExistUser(users, Username);
            if (UserExist == true)
            {
                int ReceiverId = hm.ReturnIdFromUsername(users, Username);
                int userId = ApplicationsMenus.userId;
                Console.Clear();
                Console.Write($"Write a message to ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Username);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", the maximun text limited to 250 characters");
                Console.WriteLine("\n");
                me.SendMessage(userId, ReceiverId);
            }
            else
            {
                hm.UserDoesNotExistMessage();
            }
        }


        public void ViewMessage()
        {
            string cmd = "select id, username from users";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            Console.Write("Type the username you want to read the messages: ");
            string Username = Console.ReadLine();
            bool existUser = hm.CheckExistUser(users, Username);
            if (existUser == true)
            {
                int ReceiverId = hm.ReturnIdFromUsername(users, Username);
                int userId = ApplicationsMenus.userId;
                Console.Clear();
                cmd = "SELECT * FROM messages WHERE deleted = 0;";
                IEnumerable<Message> messages = da.ReturnMessagesDatabase(cmd);
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
                hm.ReturnBackMessage();
            }
        }


        public void ViewNewMessage()
        {
            Console.Clear();
            int SenderId;
            int userId = ApplicationsMenus.userId;
            string cmd = "select id, username from users";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0 AND readed = 0;";
            IEnumerable<Message> messages = da.ReturnMessagesDatabase(cmd);
            var myMessage = messages.Where(m => m.receiverId == userId).ToList();
            foreach (var m in myMessage)
            {
                SenderId = m.senderId;
                string Sender = hm.ReturnUsernameFromId(users, SenderId);

                Console.Write("From: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Sender);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" - Message: " + m.messageData);
                da.ProcessMessagesDatabase(m.id, "Read_messages");
            }
            hm.ReturnBackMessage();
        }
        


        public void ViewAllMessageByUser()
        {
            Console.Clear();
            int SenderId;
            int userId = ApplicationsMenus.userId;
            string cmd = "select id, username from users";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0;";
            IEnumerable<Message> messages = da.ReturnMessagesDatabase(cmd);
            var myMessage = messages.Where(m => m.receiverId == userId).ToList();
            foreach (var m in myMessage)
            {

                SenderId = m.senderId;
                string Sender = hm.ReturnUsernameFromId(users, SenderId);

                Console.Write("From: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Sender);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" - Message: " + m.messageData);
                da.ProcessMessagesDatabase(m.id, "Read_messages");
                Console.Write("\n");
            }
            hm.ReturnBackMessage();
        }


        public void ViewAllMessage()
        {
            Console.Clear();
            int SenderId;
            int receiverId;
            string Sender;
            string receiver;
            string cmd = "select id, username from users";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = da.ReturnMessagesDatabase(cmd);
            foreach (var m in messages)
            {
                SenderId = m.senderId;
                receiverId = m.receiverId;
                Sender = hm.ReturnUsernameFromId(users, SenderId);
                receiver = hm.ReturnUsernameFromId(users, receiverId);

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
            hm.ReturnBackMessage();
        }


        public void EditMessage()
        {
            string cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = da.ReturnMessagesDatabase(cmd);
            Console.Write("Type the id of message you want to update: ");

            int id;
            string ID = Console.ReadLine();
            bool result = int.TryParse(ID, out id);
            if (result)
            {
                bool MessageExist = hm.CheckExistMessage(id);
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
                    me.SendMessage(SenderId, ReceiverId);
                }
                else
                {
                    hm.MessageDoesNotExist();
                }
            }
            else
            {
                hm.IncorrectMessage();
            }
        }

        public void DeleteMessage()
        {
            Console.Write("Type the id of message you want to delete: ");

            int id;
            string ID = Console.ReadLine();
            bool result = int.TryParse(ID, out id);
            if (result)
            {
                bool MessageExist = hm.CheckExistMessage(id);
                if (MessageExist == true)
                {
                    Console.Clear();
                    da.ProcessMessagesDatabase(id, "Delete_messages");
                }
                else
                {
                    hm.MessageDoesNotExist();
                }
            }
            else
            {
                hm.IncorrectMessage();
            }

        }






        public void CreateUser()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username: ");
            string username = Console.ReadLine();
            bool userExist = hm.CheckNoExistUser(username);
            if (userExist == false)
            {
                string password = hm.SamePassword();
                Console.Clear();
                string role = am.RoleMenu();
                DBDictionary.Add("@username", username);
                DBDictionary.Add("@pass", password);
                DBDictionary.Add("@role", role);
                da.ProcedureDatabase(DBDictionary, "Insert_Users");
            }
        }



        public void ViewUser()
        {
            string cmd = "select username, role from users where deleted = 0";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine("Username: " + u.username + " - " + "Role: " + u.role);
            }
            hm.ReturnBackMessage();
        }



        public void DeleteUser()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            bool UserExist = hm.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                DBDictionary.Add("@username", username);
                da.ProcedureDatabase(DBDictionary, "Delete_Users");
            }
            else
            {
                hm.UserDoesNotExistMessage();
            }
        }


        public void UpdateUserName()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool userNotExist;
            bool UserExist = hm.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                Console.Write("Type a new username: ");
                string newUsername = Console.ReadLine();
                userNotExist = hm.CheckNoExistUser(newUsername);
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
                hm.UserDoesNotExistMessage();
            }
        }


        public void UpdatePassword()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool UserExist = hm.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                string password = hm.SamePassword();
                Console.Clear();
                DBDictionary.Add("username", username);
                DBDictionary.Add("newPassword", password);
                da.ProcedureDatabase(DBDictionary, "Update_Users_By_Password");
            }
            else
            {
                hm.UserDoesNotExistMessage();
            }
        }



        public void UpdateRole()
        {
            Dictionary<string, string> DBDictionary = new Dictionary<string, string>();
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            bool UserExist = hm.CheckExistUser(username);
            if (UserExist == true)
            {
                Console.Clear();
                string role = am.RoleMenu();
                DBDictionary.Add("username", username);
                DBDictionary.Add("newRole", role);
                da.ProcedureDatabase(DBDictionary, "Update_Users_By_Role");
            }
            else
            {
                hm.UserDoesNotExistMessage();
            }
        }
    }
}
