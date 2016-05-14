using System.IO;
using Microsoft.AspNet.SignalR;


namespace RemoteExplorerServerInfrastructure
{
    public class RemoteExplorerHub: Hub
    {
        #region Methods
        public void EnumerateFolder(string folderPath)
        {
            Clients.Caller.enumerateFolder(Directory.GetFileSystemEntries(folderPath));
        }
        #endregion
    }
}