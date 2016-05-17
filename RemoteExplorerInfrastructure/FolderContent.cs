namespace RemoteExplorerInfrastructure
{
    public class FolderContent
    {
        #region  Properties & Indexers
        public FileSystemEntry[] Content { get; set; }
        public FolderEntry Folder { get; set; }
        public FolderEntry Parent { get; set; }
        #endregion
    }
}