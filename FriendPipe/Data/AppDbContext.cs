using FriendPipe.Models;
using FriendPipeApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipe.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
    .HasMany(c => c.Posts)
    .WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);


            builder.Entity<UserFollow>()
           .HasOne(l => l.SourceUser)
           .WithMany(a => a.Following)
           .HasForeignKey(l => l.SourceUserId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollow>()
                   .HasOne(l => l.FollowedUser)
                   .WithMany(a => a.Followers)
                   .HasForeignKey(l => l.FollowedUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(x => x.Post).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
