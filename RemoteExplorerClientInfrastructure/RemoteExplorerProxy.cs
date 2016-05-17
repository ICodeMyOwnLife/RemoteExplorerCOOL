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
        public string File { get; private set; }
        public FolderContent FolderContent { get; private set; }
        #endregion


        #region Methods
        public async Task OpenFolder(FileSystemEntry folderEntry)
        {
            await _hubProxy.Invoke("OpenFolder", folderEntry);
        }

        public async Task GetFile(FileSystemEntry fileEntry)
        {
            await Task.Delay(50);
            File = @"D:\a.txt";
        }
        #endregion


        #region Override
        protected override void InitializeProxy()
        {
            base.InitializeProxy();
            _hubProxy.On<FolderContent>("openFolder", folderContent =>
            {
                FolderContent = folderContent;
            });
        }
        #endregion
    }
}