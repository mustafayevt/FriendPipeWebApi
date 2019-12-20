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
        int AddUserFollow(int sourceUserId, int followUserId);
        List<User> GetUserFollowing(int sourceUserId);
        List<User> GetUserFollowers(int userId);
    }
}
