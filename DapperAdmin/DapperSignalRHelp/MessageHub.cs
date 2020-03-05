using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSignalRHelp
{
    public class MessageHub : Hub
    {
        public void Welcome(string name)
        {
            Clients.All.listen(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + name + ":" + Context.ConnectionId);
        }
    }
}
