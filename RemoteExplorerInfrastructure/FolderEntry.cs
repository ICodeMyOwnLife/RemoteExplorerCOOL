namespace RemoteExplorerInfrastructure
{
    public class FolderEntry: FileSystemEntryBase
    {
        #region  Constructors & Destructor
        internal FolderEntry(string fullPath): base(fullPath) { }

        public FolderEntry() { }
        #endregion


        #region  Properties & Indexers
        public override bool IsFileEntry { get; set; } = false;
        public override bool IsFolderEntry { get; set; } = true;
        #endregion
    }
}