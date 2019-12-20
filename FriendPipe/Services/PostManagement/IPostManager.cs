using FriendPipe.Models;
using FriendPipeApi.Models;
using FriendPipeApi.Services.UserFollowManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Services.PostManagement
{
    public interface IPostManager
    {
        List<Post> GetFollowingUserPosts(int UserId);
        List<Post> GetUserPosts(int UserId);
        int AddPost(Post newPost);
        List<Comment> GetPostComments(int PostId);
        int AddComment(Comment newComment);
    }
}
