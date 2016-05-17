using System;
using System.IO;
using System.Linq;
using Microsoft.AspNet.SignalR;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerServerInfrastructure
{
    public class RemoteExplorerHub: Hub
    {
        #region Methods
        public void OpenFolder(FileSystemEntry folderEntry)
        {
            try
            {
                Clients.Caller.openFolder(GetFolderContent(folderEntry));
            }
            catch (Exception exception)
            {
                Clients.Caller.error(exception.Message);
            }
        }
        #endregion


        #region Implementation
        private FolderContent GetFolderContent(FileSystemEntry folderEntry)
        {
            if (folderEntry.IsRoot()) return GetRootContent();
            var folderPath = folderEntry.FullPath;
            if (folderEntry.Kind != FileSystemKind.Folder)
            {
                Clients.Caller.error($"{folderPath} is not a folder.");
                return null;
            }
            return new FolderContent
            {
                Folder = folderEntry,
                Parent = GetParent(folderEntry),
                Content = Directory.EnumerateDirectories(folderPath)
                                   .Concat(Directory.EnumerateFiles(folderPath))
                                   .Select(p => new FileSystemEntry(p)).ToArray()
            };
        }

        private static FileSystemEntry GetParent(FileSystemEntry entry)
        {
            if (entry.IsRoot()) return null;

            var parentFolder = Path.GetDirectoryName(entry.FullPath);
            return string.IsNullOrEmpty(parentFolder)
                       ? FileSystemEntry.Root : new FileSystemEntry(parentFolder);
        }

        private static FolderContent GetRootContent()
            => new FolderContent
            {
                Parent = null,
                Folder = FileSystemEntry.Root,
                Content = Directory.GetLogicalDrives()
                                   .Where(Directory.Exists)
                                   .Select(p => new FileSystemEntry(p)).ToArray()
            };
        #endregion
    }
}