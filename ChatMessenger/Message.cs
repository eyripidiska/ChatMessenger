using System;

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

    }
}
