using System.Windows;
using System.Windows.Controls;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerWindowClient
{
    public class FileSystemExtryDataTemplateSelector: DataTemplateSelector
    {
        #region Override
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            var fileSystemEntry = item as FileSystemEntryBase;
            return fileSystemEntry?.IsFileEntry == true ? element?.FindResource("FileEntryDataTemplate") as DataTemplate
                       : element?.FindResource("FolderEntryDataTemplate") as DataTemplate;
        }
        #endregion
    }
}