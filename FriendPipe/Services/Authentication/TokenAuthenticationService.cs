using FriendPipe.Data;
using FriendPipe.Dtos.Authentication;
using FriendPipe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FriendPipe.Services
{
    public class TokenAuthenticationService : IAuthenticationService
    {
        public IConfiguration Configuration { get; }

        private readonly AppDbContext appDbContext;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public TokenAuthenticationService(
            IConfiguration configuration,
            ITokenService tokenService,
            AppDbContext appDbContext,
            SignInManager<User> signInManager)
        {
            Configuration = configuration;
            _tokenService = tokenService;
            this.appDbContext = appDbContext;
            this._signInManager = signInManager;
        }

        public async Task<SignInResponse>IsAuthenticated(SignInDto request)
        {
            
            
            var user = appDbContext.Users.SingleOrDefault(u => u.UserName == request.username);
            if (user == null) return null;

            var loggedin = await _signInManager.CheckPasswordSignInAsync(user, request.password, true);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwtToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
                user.WebRefreshToken = refreshToken;
            appDbContext.SaveChanges();

            return new SignInResponse {IsSigned = loggedin.Succeeded, Token = new RefreshAccessToken { AccessToken = jwtToken, RefreshToken = refreshToken } };
        }
        public async Task<SignInResponse> RefreshToken(RefreshAccessToken model)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            var username = principal.Identity.Name;
            var user = appDbContext.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null || model.RefreshToken != user.WebRefreshToken)
                return null;
            var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
                user.WebRefreshToken = newRefreshToken;
                await appDbContext.SaveChangesAsync();
            

            return new SignInResponse() { IsSigned = true, Token = new RefreshAccessToken() { AccessToken = newJwtToken, RefreshToken = newRefreshToken} };
        }
    }
}
