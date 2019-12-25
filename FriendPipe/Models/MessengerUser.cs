using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Models
{
    public class MessengerUser
    {
        public string userName { get; set; }
        public string connectionId { get; set; }
        public int unreadedMessages { get; set; }
        public List<Message> messages { get; set; } = new List<Message>();
    }
}
