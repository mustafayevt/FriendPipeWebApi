﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string User { get; set; }
        public DateTime PostedDate { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
