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
            bool UserExist = HelpMethods.ReturnExistUser(users, Username);
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
        }


        public static void ViewMessage()
        {
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.Write("Type the user you want to read the messages: ");
            string Username = Console.ReadLine();
            bool existUser = HelpMethods.ReturnExistUser(users, Username);
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
                foreach(var m in messages)
                {
                    if (m.senderId == userId)
                    {
                        Console.WriteLine("You" + ": " + m.messageData);
                    }
                    else
                    {
                       Console.WriteLine(Username + ": " + m.messageData);
                    }
                }
                Console.Write("\n");
                Console.WriteLine("Press any key to return back");
                Console.ReadLine();
                Console.Clear();
            }
        }


        //public static void ViewNewMessage()
        //{
        //    string cmd1 = "SELECT * FROM messages WHERE receiverId = @senderId AND deleted = 0 AND readed = 0;";
        //    string cmd2 = "select * from users where deleted = 0";
        //    int userId = ApplicationsMenus.userId;
        //    int SenderId;
        //    string username;
        //    IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd2);
        //    IEnumerable<dynamic> messages = DatabasesAccess.ReadMessagesDatabase(cmd1, userId);
        //    Console.Clear();
        //    foreach (var m in messages)
        //    {
        //        SenderId = m.senderId;
        //        username = users
        //            .Where(x => SenderId == x.id)
        //            .Select(x => x.username)
        //            .FirstOrDefault();

        //        Console.WriteLine(username + ": " + m.messageData);
        //        Console.Write("\n");
        //        Console.WriteLine("Press any key to read the next");
        //        Console.ReadLine();
        //        Console.Clear();
        //    }
        //}


        public static void ViewMessageMethod()
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

                Console.WriteLine("Id: " + m.id + " - From: " + Sender + " - To: " + receiver + " - Message: " + m.messageData);
            }
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
            Console.Clear();
        }


        public static void EditMessageMethod()
        {
            string cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<dynamic> messages = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.Write("Type the id of message you want to update: ");
            int id = int.Parse(Console.ReadLine());

            var senderId = messages
                    .Where(x => x.id == id)
                    .Select(x => x.senderId);
            int SenderId = Convert.ToInt32(senderId.FirstOrDefault());

            var receiverId = messages
                .Where(x => x.id == id)
                .Select(x => x.receiverId);
            int ReceiverId = Convert.ToInt32(receiverId.FirstOrDefault());
            DatabasesAccess.DeleteMessagesDatabase(id);
            Console.WriteLine($"Write the new message the maximun text limited to 250 characters");
            Console.WriteLine("\n");
            Message.SendMessageMethod(SenderId, ReceiverId);
        }

        public static void DeleteMessageMethod()
        {
            //ViewMessageMethod();
            Console.Write("Type the id of message you want to delete: ");
            int id = int.Parse(Console.ReadLine());
            //IEnumerable<dynamic> messages = DatabasesAccess.ReturnQueryDatabase(cmd);
            DatabasesAccess.DeleteMessagesDatabase(id);
        }


        public static void CreateUserMethod()
        {
            string cmd = "select * from users";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            string username = HelpMethods.ReturnNoExistUser(users);
            string password = HelpMethods.SamePasswordMethod();
            Console.Clear();
            string role = ApplicationsMenus.RoleMenuMethod();
            DatabasesAccess.InsertUsersDatabase(username, password, role);
        }



        public static void ViewUserMethod()
        {
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            Console.WriteLine("The users are:");
            Console.Write("\n");
            foreach (var u in users)
            {
                Console.WriteLine("Username: " + u.username + " - " + "Role: " +  u.role);
            }
            Console.Write("\n");
            Console.WriteLine("Press any key to return back");
            Console.ReadKey();
            Console.Clear();
        }



        public static void DeleteUserMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to delete: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = HelpMethods.CheckExistUser(users, username);
            if (UserExist == false)
            {
                Console.Clear();
                Console.Write("The user does not exist");
                Console.WriteLine("\n");
            }
            Console.Clear();
            DatabasesAccess.DeleteUsersDatabase(username);
        }



        public static void UpdatePasswordMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = HelpMethods.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.Clear();
                string password = HelpMethods.SamePasswordMethod();
                Console.Clear();
                DatabasesAccess.UpdatePasswordDatabase(username, password);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The user does not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n");
            }
        }



        public static void UpdateRoleMethod()
        {
            string cmd = "select * from users";
            Console.Write("Type the username you want to update: ");
            string username = Console.ReadLine();
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            bool UserExist = HelpMethods.CheckExistUser(users, username);
            if (UserExist == true)
            {
                Console.Clear();
                string role = ApplicationsMenus.RoleMenuMethod();
                DatabasesAccess.UpdateRoleDatabase(username, role);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The user does not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n");
            }
        }
    }
}
