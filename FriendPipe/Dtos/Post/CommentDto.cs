using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Dtos.Post
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; } 
    }
}
