using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    public class Message
    {
        public int id { get; set; }

        public int senderId { get; set; }

        public int receiverId { get; set; }

        public DateTime dateOfSubmission { get; set; }

        public string messageData { get; set; }

        public bool deleted { get; set; }


        DatabasesAccess da = new DatabasesAccess();
        HelpMethods hm = new HelpMethods();

        public void SendMessage(int userId, int receiverId)
        {
            string message = Console.ReadLine();
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





        public void FindUserName(int SenderId, int receiverId, string message)
        {
            string Sender;
            string receiver;
            string cmd = "select * from users where deleted = 0";
            IEnumerable<User> users = da.ReturnUsersDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<Message> messages = da.ReturnMessagesDatabase(cmd);
            Sender = hm.ReturnUsernameFromId(users, SenderId);
            receiver = hm.ReturnUsernameFromId(users, receiverId);

            FilesAccess.Files(Sender, receiver, message, SenderId, receiverId);
        }
    }
}
