using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using RemoteExplorerWebServer;

[assembly: OwinStartup(typeof(RemoteExplorerStartup))]


namespace RemoteExplorerWebServer
{
    public class RemoteExplorerStartup
    {
        #region Methods
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
        #endregion
    }
}