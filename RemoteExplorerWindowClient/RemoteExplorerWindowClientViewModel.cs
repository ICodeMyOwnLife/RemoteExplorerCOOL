using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using RemoteExplorerClientInfrastructure;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerWindowClient
{
    public class RemoteExplorerWindowClientViewModel: SignalRClientViewModelBase<RemoteExplorerProxy>
    {
        #region Fields
        private FileSystemEntryBase[] _entries;
        private FileSystemEntryBase _selectedEntry = FileSystemEntryBase.CreateEntry("");
        #endregion


        #region  Constructors & Destructor
        public RemoteExplorerWindowClientViewModel(): base(new RemoteExplorerProxy())
        {
            OpenEntryAsyncCommand = DelegateCommand<FileSystemEntryBase>.FromAsyncHandler(OpenEntryAsync);
            OpenFileAsyncCommand = DelegateCommand<FileSystemEntryBase>.FromAsyncHandler(OpenFileAsync);
            OpenFolderAsyncCommand =
                DelegateCommand<FileSystemEntryBase>.FromAsyncHandler(OpenFolderAsync);
            UpFolderAsyncCommand = DelegateCommand.FromAsyncHandler(UpFolderAsync);
        }
        #endregion


        #region  Properties & Indexers
        public FileSystemEntryBase[] Entries
        {
            get { return _entries; }
            private set { SetProperty(ref _entries, value); }
        }

        public ICommand OpenEntryAsyncCommand { get; }
        public ICommand OpenFileAsyncCommand { get; }

        public ICommand OpenFolderAsyncCommand { get; }

        public FileSystemEntryBase SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }

        public ICommand UpFolderAsyncCommand { get; }
        #endregion


        #region Methods
        public async Task OpenEntryAsync(FileSystemEntryBase entry)
        {
            if ((entry = entry ?? SelectedEntry) == null) return;
            if (entry.IsFileEntry) await OpenFileAsync(entry);
            else await OpenFolderAsync(entry);
        }

        public async Task OpenFileAsync(FileSystemEntryBase entry)
        {
            if ((entry = entry ?? SelectedEntry) == null) return;
            await _proxy.GetFile(entry);
            var file = _proxy.File;
            Process.Start(file);
        }

        public async Task OpenFolderAsync(FileSystemEntryBase entry)
        {
            if ((entry = entry ?? SelectedEntry) == null) return;
            await _proxy.EnumerateFolder(entry);
            Entries = _proxy.Entries;
        }

        public async Task UpFolderAsync()
            => await OpenFolderAsync(SelectedEntry = FileSystemEntryBase.Root);
        #endregion
    }
}


// TODO: Add Enter / DoubleClick command