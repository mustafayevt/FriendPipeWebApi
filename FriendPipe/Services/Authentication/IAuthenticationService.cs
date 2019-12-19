using FriendPipe.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipe.Services
{
    public interface IAuthenticationService
    {
        Task<SignInResponse> IsAuthenticated(SignInDto request);
        Task<SignInResponse> RefreshToken(RefreshAccessToken model);
    }
}
