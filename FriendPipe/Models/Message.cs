using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Models
{
    public class Message
    {
        public string senderId { get; set; }
        public string message { get; set; }
        public bool isIncoming { get; set; }
    }
}
