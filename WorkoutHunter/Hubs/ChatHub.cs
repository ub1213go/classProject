using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutHunterV2.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string uid, string str, string vit, string agi)
        {
            await Clients.All.SendAsync(uid, str, vit, agi);

        }
    }
}
