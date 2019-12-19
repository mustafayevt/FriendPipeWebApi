using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipe.Dtos.Authentication
{
    public class SignInResponse
    {
        public bool IsSigned { get; set; }
        public RefreshAccessToken Token { get; set; } = new RefreshAccessToken();
    }
}
