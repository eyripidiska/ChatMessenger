using System;
using System.IO;

namespace ChatMessenger
{
    class FilesAccess
    {
        public static void Files(string Sender, string receiver, string message, int SenderId, int receiverId)
        {
            string path = @"C:\Users\EVRI\Desktop\ChatMessenger-masternn\messages\";
            if (!File.Exists(path + SenderId + "-" + receiverId + ".txt") && !File.Exists(path + receiverId + "-" + SenderId + ".txt"))
            {
                var myFile = File.Create(path + SenderId + "-" + receiverId + ".txt");
                myFile.Close();
            }
            if (File.Exists(path + SenderId + "-" + receiverId + ".txt"))
            {
                TextWriter file = new StreamWriter((path + SenderId + "-" + receiverId + ".txt"), true);
                file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " + message + "\r\n");
                file.Close();
            }
            else if (File.Exists(path + receiverId + "-" + SenderId + ".txt"))
            {
                TextWriter file = new StreamWriter((path + receiverId + "-" + SenderId + ".txt"), true);
                file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " + message + "\r\n");
                file.Close();
            }
        }
    }
}
