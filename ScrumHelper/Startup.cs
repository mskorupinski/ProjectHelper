using Microsoft.Owin;
using Owin;



[assembly: OwinStartupAttribute(typeof(ScrumHelper.Startup))]

namespace ScrumHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
