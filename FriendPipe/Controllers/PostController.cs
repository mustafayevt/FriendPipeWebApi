using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendPipe.Controllers;
using FriendPipe.Data;
using FriendPipe.Models;
using FriendPipe.Services;
using FriendPipeApi.Dtos.Post;
using FriendPipeApi.Services.PostManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FriendPipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : MainController
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<PostController> _logger;
        private readonly IPostManager _postManager;

        public PostController(IAuthenticationService authService,
            ITokenService tokenService,
            AppDbContext appDbContext,
            UserManager<User> userManager,
            ILogger<PostController> logger,
            IPostManager postManager) : base(appDbContext, userManager)
        {
            _authService = authService;
            _tokenService = tokenService;
            _logger = logger;
            _postManager = postManager;
        }

        [Route("getposts")]
        [HttpGet]
        public IActionResult Get()
        {
            var user = _userManager.GetUserAsync(User);
            var result = _postManager.GetFollowingUserPosts(user.Result.Id).Select(x => new PostDto { Content = x.Content, Id = x.Id, PostedDate = x.PostedDate }).ToList();
            result.AddRange(_postManager.GetUserPosts(user.Result.Id).Select(x => new PostDto { Content = x.Content, Id = x.Id, PostedDate = x.PostedDate }));
            return Ok(result);
        }
    }
}