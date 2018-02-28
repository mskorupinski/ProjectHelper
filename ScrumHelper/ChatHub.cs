using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ScrumHelper
{

        public class ChatHub : Hub
        {
            public override System.Threading.Tasks.Task OnConnected()
            {
                Clients.All.user(Context.User.Identity.Name);
                return base.OnConnected();
            }
            public void Send(string message)
            {
                Clients.Caller.message("Ty: " + message);
                Clients.Others.message(Context.User.Identity.Name + ": " + message);
            }
        }
    
}