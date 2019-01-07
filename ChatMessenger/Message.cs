using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMessenger
{
    public class Message
    {
        public int _userId { get; set; }
        public int _receiverId { get; set; }
        public string _Username { get; set; }



        public Message(int userId, int receiverId, string Username)
        {
            _userId = userId;
            _receiverId = receiverId;
            _Username = Username;
        }



        public static void SendMessageMethod(int userId, int receiverId)
        {
            string message = Console.ReadLine();
            if (message.Length <= 250)
            {
                Console.Clear();
                DatabasesAccess.InsertMessagesDatabase(userId, receiverId, message);
                FindUserNameMethod(userId, receiverId, message);
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





        public static void FindUserNameMethod(int SenderId, int receiverId, string message)
        {
            string Sender;
            string receiver;
            string cmd = "select * from users where deleted = 0";
            IEnumerable<dynamic> users = DatabasesAccess.ReturnQueryDatabase(cmd);
            cmd = "SELECT * FROM messages WHERE deleted = 0";
            IEnumerable<dynamic> messages = DatabasesAccess.ReturnQueryDatabase(cmd);

            Sender = users
                .Where(x => SenderId == x.id)
                .Select(x => x.username)
                .FirstOrDefault();
            Console.WriteLine(Sender);
            Console.ReadKey();
            receiver = users
                .Where(x => receiverId == x.id)
                .Select(x => x.username)
                .FirstOrDefault();
            
            FilesAccess.Files(Sender, receiver, message);
        }
    }
}
