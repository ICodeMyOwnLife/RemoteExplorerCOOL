using System;
using System.IO;


namespace RemoteExplorerInfrastructure
{
    public class FileSystemEntry
    {
        #region Fields
        private const string ROOT_PATH = "";
        #endregion


        #region  Constructors & Destructor
        protected FileSystemEntry(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (path == "" || Directory.Exists(path))
            {
                return new FolderEntry(path);
            }
            if (File.Exists(path))
            {
                return new FileEntry(path);
            }
            throw new FileNotFoundException(path);
            FullPath = fullPath;

            if (File.Exists(fullPath))
            {
                Name = Path.GetFileName(fullPath);
            }
            else if (Directory.Exists(fullPath))
            {
                var dirInfo = new DirectoryInfo(fullPath);
                Name = dirInfo.Parent == null ? FullPath : dirInfo.Name;
            }
        }

        protected FileSystemEntry() { }
        #endregion


        #region  Properties & Indexers
        public static FileSystemEntry Root { get; } = new FileSystemEntry(ROOT_PATH);
        public string FullPath { get; set; }

        public virtual bool IsFileEntry { get; set; } = false;

        public virtual bool IsFolderEntry { get; set; } = false;
        public string Name { get; set; }
        #endregion


        #region Methods
        public bool IsRoot() => ROOT_PATH.Equals(FullPath);
        #endregion


        #region Override
        public override string ToString()
            => $"Name: {Name} - Full path: {FullPath}";
        #endregion
    }
}


//TODO: Edit FileSystemEntry
//TODO: Implement SendFile feature