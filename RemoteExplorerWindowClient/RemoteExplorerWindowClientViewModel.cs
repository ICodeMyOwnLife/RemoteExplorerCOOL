using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using RemoteExplorerClientInfrastructure;


namespace RemoteExplorerWindowClient
{
    public class RemoteExplorerWindowClientViewModel: ViewModelBase
    {
        #region Fields
        private ICommand _connectAsyncCommand;
        private ICommand _disconnectCommand;
        private string[] _entries;
        private ICommand _enumerateFolderAsyncCommand;
        private readonly RemoteExplorerProxy _proxy = new RemoteExplorerProxy();
        private string _selectedEntry;
        #endregion


        #region  Properties & Indexers
        public ICommand ConnectAsyncCommand
            => GetCommand(ref _connectAsyncCommand, async _ => await ConnectAsync() /*, _ => CanConnect*/);

        public ICommand DisconnectCommand
            => GetCommand(ref _disconnectCommand, _ => Disconnect() /*, _ => CanDisconnect*/);

        public string[] Entries
        {
            get { return _entries; }
            private set { SetProperty(ref _entries, value); }
        }

        public ICommand EnumerateFolderAsyncCommand
            =>
                GetCommand(ref _enumerateFolderAsyncCommand, async _ => await EnumerateFolderAsync()
                    /*, _ => CanEnumerateFolder*/);

        public string SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }
        #endregion


        #region Methods
        public async Task ConnectAsync() => await _proxy.ConnectAsync();
        public void Disconnect() => _proxy.Disconnect();

        public async Task EnumerateFolderAsync()
        {
            await _proxy.EnumerateFolder(SelectedEntry);
            Entries = _proxy.Entries;
        }
        #endregion
    }
}