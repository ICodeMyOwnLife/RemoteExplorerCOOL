using System.Threading.Tasks;
using System.Windows.Input;
using RemoteExplorerClientInfrastructure;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerWindowClient
{
    public class RemoteExplorerWindowClientViewModel: SignalRClientViewModelBase<RemoteExplorerProxy>
    {
        #region Fields
        private FileSystemEntryBase[] _entries;
        private ICommand _enumerateFolderAsyncCommand;
        private FileSystemEntryBase _selectedEntry = FileSystemEntryBase.CreateEntry("");
        #endregion


        #region  Constructors & Destructor
        public RemoteExplorerWindowClientViewModel(): base(new RemoteExplorerProxy()) { }
        #endregion


        #region  Properties & Indexers
        public FileSystemEntryBase[] Entries
        {
            get { return _entries; }
            private set { SetProperty(ref _entries, value); }
        }

        public ICommand EnumerateFolderAsyncCommand
            =>
                GetCommand(ref _enumerateFolderAsyncCommand,
                    async par => await EnumerateFolderAsync(par as FileSystemEntryBase ?? SelectedEntry)
                    /*, _ => CanEnumerateFolder*/);

        public FileSystemEntryBase SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }
        #endregion


        #region Methods
        public async Task EnumerateFolderAsync(FileSystemEntryBase entry)
        {
            await _proxy.EnumerateFolder(entry);
            Entries = _proxy.Entries;
        }
        #endregion
    }
}


// TODO: Add Enter / DoubleClick command