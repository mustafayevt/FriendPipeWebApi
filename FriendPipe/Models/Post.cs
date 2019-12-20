using FriendPipeApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipe.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public virtual List<Comment> Comments { get; set; }
    }
}
