using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using RemoteExplorerServerInfrastructure;

[assembly: OwinStartup(typeof(RemoteExplorerStartup))]


namespace RemoteExplorerServerInfrastructure
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