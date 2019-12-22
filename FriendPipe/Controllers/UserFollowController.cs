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
using Toya.Dtos.Authentication;

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
                var user = _userManager.GetUserAsync(User).Result;
                return Ok(_userFollowManager.GetUserFollowers(user.Id).Select(x=> new UserDto { Email = x.Email,Name = x.Name,Surname = x.Surname,Username= x.UserName}));
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
                var user = _userManager.GetUserAsync(User).Result;
                return Ok(_userFollowManager.GetUserFollowing(user.Id).Select(x => new UserDto { Email = x.Email, Name = x.Name, Surname = x.Surname, Username = x.UserName }));
            }
            catch (Exception)
            {
                NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("addfollow")]
        public IActionResult AddFollow(string followUserName)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                return Ok(_userFollowManager.AddUserFollow(user.Id, followUserName));
            }
            catch (Exception ex)
            {
                NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getrandomuser")]
        public IActionResult GetRandomUsers(int userCount)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                return Ok(_userFollowManager.GetRandomUsers(user.Id, userCount).Select(x => new UserDto { Email = x.Email, Name = x.Name, Surname = x.Surname, Username = x.UserName }));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        [Route("getnotfolloweduser")]
        public IActionResult GetNotFollowedUsers()
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                return Ok(_userFollowManager.GetNotFollowedUsers(user.Id).Select(x => new UserDto { Email = x.Email, Name = x.Name, Surname = x.Surname, Username = x.UserName }));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
    }
}