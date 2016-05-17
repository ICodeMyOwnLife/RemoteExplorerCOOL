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
        private FolderContent _folderContent;
        private FileSystemEntry _selectedEntry = FileSystemEntry.CreateEntry("");
        #endregion


        #region  Constructors & Destructor
        public RemoteExplorerWindowClientViewModel(): base(new RemoteExplorerProxy())
        {
            OpenEntryAsyncCommand = DelegateCommand<FileSystemEntry>.FromAsyncHandler(OpenEntryAsync);
            OpenFileAsyncCommand = DelegateCommand<FileSystemEntry>.FromAsyncHandler(OpenFileAsync);
            OpenFolderAsyncCommand =
                DelegateCommand<FileSystemEntry>.FromAsyncHandler(OpenFolderAsync);
            UpFolderAsyncCommand = DelegateCommand.FromAsyncHandler(UpFolderAsync);
        }
        #endregion


        #region  Properties & Indexers
        public FolderContent FolderContent
        {
            get { return _folderContent; }
            private set { SetProperty(ref _folderContent, value); }
        }

        public ICommand OpenEntryAsyncCommand { get; }
        public ICommand OpenFileAsyncCommand { get; }
        public ICommand OpenFolderAsyncCommand { get; }

        public FileSystemEntry SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }

        public ICommand UpFolderAsyncCommand { get; }
        #endregion


        #region Methods
        public async Task OpenEntryAsync(FileSystemEntry entry)
        {
            if ((entry = entry ?? SelectedEntry) == null) return;
            if (entry.IsFileEntry) await OpenFileAsync(entry);
            else await OpenFolderAsync(entry);
        }

        public async Task OpenFileAsync(FileSystemEntry entry)
        {
            if ((entry = entry ?? SelectedEntry) == null) return;
            await _proxy.GetFile(entry);
            var file = _proxy.File;
            Process.Start(file);
        }

        public async Task OpenFolderAsync(FileSystemEntry entry)
        {
            if ((entry = entry ?? SelectedEntry) == null) return;
            await _proxy.OpenFolder(entry);
            FolderContent = _proxy.FolderContent;
        }

        public async Task UpFolderAsync()
            => await OpenFolderAsync(SelectedEntry = FolderContent.Parent);
        #endregion
    }
}


// TODO: Add Enter / DoubleClick command