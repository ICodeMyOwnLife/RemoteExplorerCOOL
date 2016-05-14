using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerClientInfrastructure
{
    public class RemoteExplorerProxy: SignalRProxyBase
    {
        #region  Constructors & Destructor
        public RemoteExplorerProxy(): base(RemoteExplorerConfig.GetSignalRUrl()) { }
        #endregion


        #region  Properties & Indexers
        public FileSystemEntryBase[] Entries { get; private set; }
        #endregion


        #region Methods
        public async Task EnumerateFolder(FileSystemEntryBase folderEntry)
        {
            await _hubProxy.Invoke("EnumerateFolder", folderEntry);
        }
        #endregion


        #region Override
        protected override void InitializeProxy()
        {
            base.InitializeProxy();
            _hubProxy.On<FileSystemEntryBase[]>("enumerateFolder", entries =>
            {
                Entries = entries;
            });
        }
        #endregion
    }
}