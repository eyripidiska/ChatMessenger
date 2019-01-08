using System;
using System.IO;

namespace ChatMessenger
{
    class FilesAccess
    {
        public static void Files(string Sender, string receiver, string message)
        {
            string path = @"C:\Users\EVRIPIDIS\Desktop\ChatMessenger\";
            if (!File.Exists(path + Sender + "-" + receiver + ".txt") && !File.Exists(path + receiver + "-" + Sender + ".txt"))
            {
                File.Create(path +  Sender + "-" + receiver + ".txt");
                StreamWriter file = new StreamWriter((path + Sender + "-" + receiver + ".txt"), true);
                
                file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " +  message + "\r\n");
                file.Close();
            }
            else if (File.Exists(path + Sender + "-" + receiver + ".txt"))
            {
                TextWriter file = new StreamWriter((path + Sender + "-" + receiver + ".txt"), true);
                file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " + message + "\r\n");
                file.Close();
            }
            else if (File.Exists(path + receiver + "-" + Sender + ".txt"))
            {
                TextWriter file = new StreamWriter((path + receiver + "-" + Sender + ".txt"), true);
                file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " + message + "\r\n");
                file.Close();
            }
        }
    }
}
