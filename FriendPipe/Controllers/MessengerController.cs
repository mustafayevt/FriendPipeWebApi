using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendPipe.Controllers;
using FriendPipe.Data;
using FriendPipe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FriendPipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : MainController
    {
        private readonly ILogger<MessengerController> _logger;

        public MessengerController(ILogger<MessengerController> logger,
            AppDbContext appDbContext,
            UserManager<User> userManager):base(appDbContext,userManager)
        {
            _logger = logger;
        }
    }
}