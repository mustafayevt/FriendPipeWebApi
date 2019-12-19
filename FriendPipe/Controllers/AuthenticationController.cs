using FriendPipe.Data;
using FriendPipe.Dtos.Authentication;
using FriendPipe.Models;
using FriendPipe.Services;
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

        public AuthenticationController(IAuthenticationService authService,
            ITokenService tokenService,
            AppDbContext appDbContext,
            UserManager<User> userManager,
            ILogger<AuthenticationController> logger) : base(appDbContext, userManager)
        {
            _authService = authService;
            _tokenService = tokenService;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            //return new string[] { "value1", "value2" };
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
                if (result == null) return BadRequest("Wrong Username");
                if (result.IsSigned)
                    return Ok(result);
                return BadRequest("Wrong Password");

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

        // helper action
        [HttpPost]
        [Route("usersignup")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto model)
        {
            var user = new User { UserName = model.Username,Name=model.Name,Surname = model.Surname,Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost, Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            var user = _appDbContext.Users.SingleOrDefault(u => u.UserName == username);

            if (user == null) return BadRequest();

            //user.web = null;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
