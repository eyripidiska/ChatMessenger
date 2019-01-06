using System;

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





        public static void ReadMessageMethod(int userId, int receiverId, string username)
        {

        }
    }   
}
