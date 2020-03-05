using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DapperAdminApi.Common.SignalR
{
    [HubName("messagehub")]
    public class MessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.addMessageToList("你好");
        }

        public void NewChatMessage(string name, string message)
        {
            Clients.All.addMessageToList(name, message);
        }
    }
}