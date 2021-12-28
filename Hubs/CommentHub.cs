using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(string TaskTitle, string CommentContent, string CommentFrom)
        {
            await Clients.All.SendAsync("RecieveCooment", TaskTitle , CommentContent , CommentFrom);
        }
        public async Task sendNotification(string MessageContent , int UserId)
        {
            await Clients.All.SendAsync("AddNotification", MessageContent, UserId);
        }
    }
}
