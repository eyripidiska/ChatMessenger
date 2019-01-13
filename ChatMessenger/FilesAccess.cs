using System;
using System.IO;

namespace ChatMessenger
{
    class FilesAccess
    {
        public static void Files(string Sender, string receiver, string message, int SenderId, int receiverId)
        {
            string path = @"C:\Users\EVRIPIDIS\Desktop\PROJECT\history\";
            if (!File.Exists(path + SenderId + "-" + receiverId + ".txt") && !File.Exists(path + receiverId + "-" + SenderId + ".txt"))
            {
                try
                {
                    var myFile = File.Create(path + SenderId + "-" + receiverId + ".txt");
                    myFile.Close();
                }
                catch (DirectoryNotFoundException dirExe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The folder with history was not found");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n");
                }
                catch (IOException ΙΟExe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong with the history folder");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n");
                }
            }
            if (File.Exists(path + SenderId + "-" + receiverId + ".txt"))
            {
                try
                {
                    TextWriter file = new StreamWriter((path + SenderId + "-" + receiverId + ".txt"), true);
                    file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " + message + "\r\n");
                    file.Close();
                }
                catch (DirectoryNotFoundException dirExe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong with the history file");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n");
                }
            }
            else if (File.Exists(path + receiverId + "-" + SenderId + ".txt"))
            {
                try
                {
                    TextWriter file = new StreamWriter((path + receiverId + "-" + SenderId + ".txt"), true);
                    file.Write("Date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - From: " + Sender + " - To: " + receiver + " - Message: " + message + "\r\n");
                    file.Close();
                }
                catch (DirectoryNotFoundException dirExe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong with the history file");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n");
                }
            }
        }
    }
}
