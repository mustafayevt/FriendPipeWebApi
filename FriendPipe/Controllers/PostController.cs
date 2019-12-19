using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendPipe.Controllers;
using FriendPipe.Data;
using FriendPipe.Models;
using FriendPipe.Services;
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
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PostController> _logger;

        public PostController(IAuthenticationService authService,
            ITokenService tokenService,
            AppDbContext appDbContext,
            UserManager<User> userManager,
            ILogger<PostController> logger) : base(appDbContext, userManager)
        {
            _authService = authService;
            _tokenService = tokenService;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _logger = logger;
        }

        [Route("getposts")]
        [HttpGet]
        public async  Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);
            var posts = _appDbContext.Posts.Where(x => x.Id == user.Id).ToList();
            return Ok(posts);
        }
    }
}