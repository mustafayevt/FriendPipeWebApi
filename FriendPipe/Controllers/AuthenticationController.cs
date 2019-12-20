using FriendPipe.Data;
using FriendPipe.Dtos.Authentication;
using FriendPipe.Models;
using FriendPipe.Services;
using FriendPipeApi.Services.UserFollowManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Toya.Dtos.Authentication;

namespace FriendPipe.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : MainController
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserFollowManager _userFollowManager;

        public AuthenticationController(IAuthenticationService authService,
            ITokenService tokenService,
            AppDbContext appDbContext,
            UserManager<User> userManager,
            ILogger<AuthenticationController> logger,
            IUserFollowManager userFollowManager) : base(appDbContext, userManager)
        {
            _authService = authService;
            _tokenService = tokenService;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _logger = logger;
            _userFollowManager = userFollowManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            
            //return new string[] { "value1", "value2" };
            var user = await _userManager.GetUserAsync(User);
            
            var a = await _userManager.GetUserAsync(User);
            return Ok(a);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin([FromBody]SignInDto request)
        {
            try
            {
                var result = await _authService.IsAuthenticated(request);
                if (result == null) return BadRequest("Username or Password is Wrong!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshAccessToken model)
        {
            var newRefreshToken = await _authService.RefreshToken(model);
            if (newRefreshToken == null) return BadRequest();

            return Ok(newRefreshToken);
            
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto model)
        {
            var user = new User { UserName = model.Username,Name=model.Name,Surname = model.Surname,Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var Token = await _authService.IsAuthenticated(new SignInDto { Password = model.Password, Username = model.Username });
                return Ok(Token);
            }

            return BadRequest();
        }

        [HttpPost, Microsoft.AspNetCore.Authorization.Authorize]
        [Route("logout")]
        public async Task<IActionResult> Revoke()
        {
            var user = await _userManager.GetUserAsync(User);


            if (user == null) return BadRequest();

            user.WebRefreshToken = null;

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
