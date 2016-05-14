using System.Configuration;


namespace RemoteExplorerInfrastructure
{
    public class RemoteExplorerConfig
    {
        public static string GetSignalRUrl()
        {
            return ConfigurationManager.AppSettings["signalRUrl"];
        }

        public static string GetHubName()
        {
            return ConfigurationManager.AppSettings["hubName"];
        }
    }
}