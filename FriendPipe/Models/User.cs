using FriendPipeApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipe.Models
{
    public class User:IdentityUser<int>
    {
        public string WebRefreshToken { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<UserFollow> Following { get; set; }
        public virtual List<UserFollow> Followers { get; set; }
    }
}
