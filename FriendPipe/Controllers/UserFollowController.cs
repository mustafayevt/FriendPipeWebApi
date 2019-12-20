using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendPipe.Controllers;
using FriendPipe.Data;
using FriendPipe.Models;
using FriendPipeApi.Services.UserFollowManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FriendPipeApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserFollowController : MainController
    {
        private readonly ILogger<UserFollowController> _logger;
        private readonly IUserFollowManager _userFollowManager;

        public UserFollowController(AppDbContext appDbContext, UserManager<User> userManager, ILogger<UserFollowController> logger, IUserFollowManager userFollowManager):base(appDbContext,userManager)
        {
            _logger = logger;
            _userFollowManager = userFollowManager;
        }


        [HttpGet]
        [Route("getfollowers")]
        public IActionResult GetFollowers()
        {
            try
            {
                var user = _userManager.GetUserAsync(User);
                return Ok(_userFollowManager.GetUserFollowers(user.Result.Id));
            }
            catch (Exception)
            {
                NotFound();
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("getfollowings")]
        public IActionResult GetFollowings()
        {
            try
            {
                var user = _userManager.GetUserAsync(User);
                return Ok(_userFollowManager.GetUserFollowing(user.Result.Id));
            }
            catch (Exception)
            {
                NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("addfollow")]
        public IActionResult AddFollow(int followUserId)
        {
            try
            {
                var user = _userManager.GetUserAsync(User);
                return Ok(_userFollowManager.AddUserFollow(user.Result.Id,followUserId));
            }
            catch (Exception)
            {
                NotFound();
            }
            return BadRequest();
        }
    }
}