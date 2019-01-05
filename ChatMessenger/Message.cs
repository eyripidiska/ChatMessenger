using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMessenger
{
    class Message
    {
        public static void SendMessageMethod(int userId, int receiverId, string username)
        {
            Console.WriteLine($"Write a message to {username}, the maximun text limited to 250 characters");
            Console.WriteLine("\n");
            string message = Console.ReadLine();
            if (message.Length <= 250)
            {
                Console.Clear();
                DatabasesAccess.InsertMessagesDatabase(userId, receiverId, message);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("the message is over to 250 characters");
                Console.WriteLine("\n");
            }
        }
    }   
}
