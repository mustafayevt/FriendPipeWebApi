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

        [Route("posts")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var user = _userManager.GetUserAsync(User);
                var result = _postManager.GetFollowingUserPosts(user.Result.Id).Select(x => new PostDto { Content = x.Content, Id = x.Id, PostedDate = x.PostedDate, User = $"{x.User.Name} {x.User.Surname}" }).ToList();
                result.AddRange(_postManager.GetUserPosts(user.Result.Id).Select(x => new PostDto { Content = x.Content, Id = x.Id, PostedDate = x.PostedDate, User = $"{x.User.Name} {x.User.Surname}" }));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("postdetail")]
        [HttpGet]
        public IActionResult Get(int PostId)
        {
            try
            {
                //var user = _userManager.GetUserAsync(User);
                var result = _postManager.GetPostById(PostId);
                var post = new PostDto() { Content = result.Content, Id = result.Id, PostedDate = result.PostedDate, Comments = result.Comments.Select(y => new CommentDto { CommentDate = y.CommentDate, Content = y.Content, User = y.User }).ToList(), User = $"{result.User.Name} {result.User.Surname}" };
                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("myposts")]
        [HttpGet]
        public IActionResult GetUserPost()
        {
            try
            {
                var user = _userManager.GetUserAsync(User);
                var result = _postManager.GetUserPosts(user.Result.Id).Select(x => new PostDto { Content = x.Content, Id = x.Id, PostedDate = x.PostedDate, User = $"{x.User.Name} {x.User.Surname}" }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("userposts")]
        [HttpGet]
        public IActionResult GetUserPost(int UserId)
        {
            try
            {
                var result = _postManager.GetUserPosts(UserId).Select(x => new PostDto { Content = x.Content, Id = x.Id, PostedDate = x.PostedDate, User = $"{x.User.Name} {x.User.Surname}" }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("addcomment")]
        [HttpPost]
        public IActionResult AddComment(int PostId, string Comment)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var result = _postManager.AddComment(new Models.Comment() { CommentDate = DateTime.Now, Content = Comment, PostId = PostId, UserId = user.Id, User = $"{user.Name} {user.Surname}" });
                return Ok(new CommentDto { CommentDate = result.Entity.CommentDate, Content = result.Entity.Content, User = result.Entity.User,PostId = result.Entity.PostId, Id = result.Entity.Id });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("getcomments")]
        [HttpGet]
        public IActionResult GetComments(int PostId)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var result = _postManager.GetPostComments(PostId).Select(x => new CommentDto() { CommentDate = x.CommentDate, Content = x.Content, PostId = x.PostId, Id = x.Id, User = x.User });
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("addpost")]
        [HttpPost]
        public IActionResult AddPost(string newPost)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var result = _postManager.AddPost(new Post() { Content = newPost, PostedDate = DateTime.Now, User = user });
                return Ok(new PostDto { Content = result.Entity.Content, User = $"{result.Entity.User.Name} {result.Entity.User.Surname}", PostedDate = result.Entity.PostedDate,Id = result.Entity.Id });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}