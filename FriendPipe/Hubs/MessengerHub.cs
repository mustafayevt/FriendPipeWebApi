using FriendPipe.Models;
using FriendPipeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Hubs
{
    public class MessengerHub:Hub
    {
        private readonly UserManager<User> _userManager;
        public static List<MessengerUser> OnlineUsers { get; set; } = new List<MessengerUser>();
        public MessengerHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task NewMessage(string id,string message)
        {
            var msg = new Message() { message = message, senderId = Context.ConnectionId,isIncoming=true };
            await Clients.Client(id).SendAsync("NewMessage", msg);
        }
        
        public async Task SendNewConnectedUser(MessengerUser newUser)
        {
            await Clients.Others.SendAsync("NewUserConnected", newUser);
        }
        public async Task UserDisconnected(MessengerUser user)
        {
            await Clients.All.SendAsync("UserDisconnected", user);
        }
        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("GetUsers", OnlineUsers);
            var user = _userManager.GetUserAsync(Context.User).Result;
            if (OnlineUsers.FirstOrDefault(x => x.userName == user.UserName) != null) return;

            var newMessengerUser = new MessengerUser { connectionId = Context.ConnectionId, userName = user.UserName };
            await SendNewConnectedUser(newMessengerUser);
            OnlineUsers.Add(newMessengerUser);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = _userManager.GetUserAsync(Context.User).Result;
            var disconnectedUser = OnlineUsers.FirstOrDefault(x => x.userName == user.UserName);
            UserDisconnected(disconnectedUser);
            OnlineUsers.Remove(disconnectedUser);
            return base.OnDisconnectedAsync(exception);
        }

    }
}

