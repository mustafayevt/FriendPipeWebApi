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

        public UserFollowManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int AddUserFollow(int sourceUserId, string followUserName)
        {
            var result = _appDbContext.UserFollows.Add(new FriendPipeApi.Models.UserFollow() { SourceUserId = sourceUserId, FollowedUserId = _appDbContext.Users.FirstOrDefault(y => y.UserName == followUserName).Id });
            return _appDbContext.SaveChanges();
        }

        public List<User> GetNotFollowedUsers(int userId)
        {
            var result = _appDbContext.Users.Where(x => (x.Followers.FirstOrDefault(y => y.SourceUserId != userId) != null && x.Id != userId)).ToList();
            return result;
        }

        public List<User> GetRandomUsers(int userId, int userCount)
        {
            var notFollowed = GetNotFollowedUsers(userId);
            int UserCount = userCount < notFollowed.Count ? userCount : notFollowed.Count;

            var randomUsers = new List<User>();
            var random = new Random();
            for (int i = 0; i < UserCount; i++)
            {
                int index = random.Next(notFollowed.Count);
                if (randomUsers.Contains(notFollowed[index]))
                {
                    i--;
                    continue;
                }
                randomUsers.Add(notFollowed[index]);

            }
            return randomUsers;
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
