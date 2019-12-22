using FriendPipe.Data;
using FriendPipe.Models;
using FriendPipeApi.Models;
using FriendPipeApi.Services.UserFollowManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public PostManager(AppDbContext appDbContext, UserManager<User> userManager, IUserFollowManager userFollowManager)
        {
            _appDbContext = appDbContext;
            _userFollowManager = userFollowManager;
        }

        public EntityEntry<Post> AddPost(Post newPost)
        {
            var result = _appDbContext.Posts.Add(newPost);
            _appDbContext.SaveChanges();
            return result;
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

        public EntityEntry<Comment> AddComment(Comment newComment)
        {
            var result = _appDbContext.Comments.Add(newComment);
            _appDbContext.SaveChanges();
            return result;
        }

        public Post GetPostById(int PostId)
        {
            return _appDbContext.Posts.FirstOrDefault(x => x.Id == PostId);
        }
    }
}
