using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Microsoft.Owin.OwinStartup(typeof(MyEcom.Core.Startup))]
namespace MyEcom.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder App)
        {
            App.MapSignalR();
        }
    }
}
