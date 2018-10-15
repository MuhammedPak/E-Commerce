using Microsoft.AspNet.SignalR;  
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core
{
    public class ReservationHub:Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["MyEcomConStr"].ToString();

        public static void Send()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ReservationHub>();
            context.Clients.All.updateMessages();
        }


    }
}
