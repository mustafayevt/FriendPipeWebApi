﻿using FriendPipe.Models;
using FriendPipeApi.Models;
using FriendPipeApi.Services.UserFollowManagement;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Services.PostManagement
{
    public interface IPostManager
    {
        List<Post> GetFollowingUserPosts(int UserId);
        Post GetPostById(int PostId);
        List<Post> GetUserPosts(int UserId);
        EntityEntry<Post> AddPost(Post newPost);
        List<Comment> GetPostComments(int PostId);
        EntityEntry<Comment> AddComment(Comment newComment);
    }
}
