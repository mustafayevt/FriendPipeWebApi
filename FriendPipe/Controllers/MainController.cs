using FriendPipe.Data;
using FriendPipe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipe.Controllers
{
    [ApiController]
    public class MainController:ControllerBase
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;

        public MainController(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
    }
}
