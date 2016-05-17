namespace RemoteExplorerInfrastructure
{
    public class FolderContent
    {
        #region  Properties & Indexers
        public FileSystemEntry[] Content { get; set; }
        public FileSystemEntry Folder { get; set; }
        public FileSystemEntry Parent { get; set; }
        #endregion
    }
}