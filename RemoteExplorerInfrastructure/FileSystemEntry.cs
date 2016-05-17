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
        public FileSystemEntry(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            FullPath = path;

            if (path == ROOT_PATH)
            {
                Name = ROOT_PATH;
                Kind = FileSystemKind.Folder;
                return;
            }
            
            if (File.Exists(path))
            {
                Name = Path.GetFileName(path);
                Kind = FileSystemKind.File;
                return;
            }

            if (Directory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);
                Name = dirInfo.Parent == null ? FullPath : dirInfo.Name;
                Kind = FileSystemKind.Folder;
                return;
            }
            throw new FileNotFoundException(path);
        }

        public FileSystemEntry() { }
        #endregion


        #region  Properties & Indexers
        public static FileSystemEntry Root { get; } = new FileSystemEntry(ROOT_PATH);
        public string FullPath { get; set; }
        public FileSystemKind Kind { get; set; }
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