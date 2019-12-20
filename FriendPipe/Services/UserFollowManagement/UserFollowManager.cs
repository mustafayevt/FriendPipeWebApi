using FriendPipe.Data;
using FriendPipe.Models;
using FriendPipeApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Services.UserFollowManagement
{
    public class UserFollowManager : IUserFollowManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public UserFollowManager(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public int AddUserFollow(int sourceUserId, int followUserId)
        {
            var result = _appDbContext.UserFollows.Add(new FriendPipeApi.Models.UserFollow() { SourceUserId = sourceUserId, FollowedUserId = followUserId });
            return _appDbContext.SaveChanges();
        }

        public List<User> GetUserFollowers(int userId)
        {
            var result = _appDbContext.UserFollows.Where(x => x.FollowedUserId == userId).Select(y => y.SourceUser).ToList();
            return result;
        }

        public List<User> GetUserFollowing(int sourceUserId)
        {
            var result = _appDbContext.UserFollows.Where(x => x.SourceUserId == sourceUserId).Select(y => y.FollowedUser).ToList();
            return result;
        }
    }
}
