using FriendPipe.Models;
using FriendPipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Services.UserFollowManagement
{
    public interface IUserFollowManager
    {
        int AddUserFollow(int sourceUserId, string followUserName);
        List<User> GetUserFollowing(int sourceUserId);
        List<User> GetUserFollowers(int userId);
        List<User> GetRandomUsers(int userId,int userCount);
        List<User> GetNotFollowedUsers(int userId);
    }
}
