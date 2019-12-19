using FriendPipe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Models
{
    public class UserFollow
    {

        public int Id { get; set; }

        [ForeignKey(nameof(SourceUserId))]
        public virtual User SourceUser { get; set; }
        public int SourceUserId { get; set; }

        [ForeignKey(nameof(FollowedUserId))]
        public virtual User FollowedUser { get; set; }
        public int FollowedUserId { get; set; }
    }
}
