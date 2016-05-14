using System;
using System.IO;


namespace RemoteExplorerInfrastructure
{
    public class FileSystemEntryBase
    {
        #region Fields
        public static FileSystemEntryBase Root = CreateEntry("");
        #endregion


        #region  Constructors & Destructor
        protected FileSystemEntryBase(string fullPath)
        {
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

        protected FileSystemEntryBase() { }
        #endregion


        #region  Properties & Indexers
        public string FullPath { get; set; }

        public virtual bool IsFileEntry { get; set; } = false;

        public virtual bool IsFolderEntry { get; set; } = false;
        public string Name { get; set; }
        #endregion


        #region Methods
        public static FileSystemEntryBase CreateEntry(string path)
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
        }
        #endregion


        #region Override
        public override string ToString()
            => $"Name: {Name} - Full path: {FullPath}";
        #endregion
    }
}