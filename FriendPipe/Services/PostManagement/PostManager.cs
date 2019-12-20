using FriendPipe.Data;
using FriendPipe.Models;
using FriendPipeApi.Models;
using FriendPipeApi.Services.UserFollowManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Services.PostManagement
{
    public class PostManager : IPostManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserFollowManager _userFollowManager;

        public PostManager(AppDbContext appDbContext, UserManager<User> userManager,IUserFollowManager userFollowManager)
        {
            _appDbContext = appDbContext;
            _userFollowManager = userFollowManager;
        }

        public int AddPost(Post newPost)
        {
            _appDbContext.Posts.Add(newPost);
            return _appDbContext.SaveChanges();
        }

        public List<Post> GetFollowingUserPosts(int FollowingUserId)
        {
            List<Post> result = new List<Post>();
            _userFollowManager.GetUserFollowing(FollowingUserId).ForEach(x => result.AddRange(x.Posts));
            return result;
        }

        public List<Post> GetUserPosts(int UserId)
        {
            return _appDbContext.Users.FirstOrDefault(x => x.Id == UserId).Posts;
        }

        public List<Comment> GetPostComments(int PostId)
        {
            return _appDbContext.Posts.FirstOrDefault(x => x.Id == PostId).Comments;
        }

        public int AddComment(Comment newComment)
        {
            _appDbContext.Comments.Add(newComment);
            return _appDbContext.SaveChanges();
        }

        public Post GetPostById(int PostId)
        {
            return _appDbContext.Posts.FirstOrDefault(x => x.Id == PostId);
        }
    }
}
