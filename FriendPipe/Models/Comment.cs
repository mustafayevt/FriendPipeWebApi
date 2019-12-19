using FriendPipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
