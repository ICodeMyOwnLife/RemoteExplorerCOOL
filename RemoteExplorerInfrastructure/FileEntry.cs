namespace RemoteExplorerInfrastructure
{
    public class FileEntry: FileSystemEntryBase
    {
        #region  Constructors & Destructor
        internal FileEntry(string fullPath): base(fullPath) { }

        public FileEntry() { }
        #endregion


        #region  Properties & Indexers
        public override bool IsFileEntry { get; set; } = true;
        public override bool IsFolderEntry { get; set; } = false;
        #endregion
    }
}