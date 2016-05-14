using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerServerInfrastructure
{
    public class RemoteExplorerHub: Hub
    {
        #region Methods
        public void EnumerateFolder(FileSystemEntryBase folder)
        {
            var folderPath = folder.FullPath;
            if (!folder.IsFolderEntry)
            {
                Clients.Caller.error($"{folderPath} is not a folder.");
            }
            else
            {
                var entryPaths = folderPath == ""
                                     ? Directory.GetLogicalDrives().Where(Directory.Exists) : Directory.GetFileSystemEntries(folderPath);
                try
                {
                    var entries = entryPaths.Select(FileSystemEntryBase.CreateEntry).ToArray();
                Clients.Caller.enumerateFolder(entries);
                }
                catch (Exception)
                {
                    
                }
            }
        }
        #endregion


        #region Override
        public override Task OnConnected()
        {
            //EnumerateFolder(FileSystemEntryBase.CreateEntry(""));
            return base.OnConnected();
        }
        #endregion
    }
}