namespace PleOps.GamePatcher.Poc.Pages.Designer;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

public partial class DesignerSelectionView : UserControl
{
    public DesignerSelectionView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is DesignerSelectionViewModel viewModel) {
            viewModel.AskOpenFile.RegisterHandler(SelectOpenFileAsync);
        }

        base.OnDataContextChanged(e);
    }

    private async Task<IStorageFile?> SelectOpenFileAsync()
    {
        var options = new FilePickerOpenOptions {
            AllowMultiple = false,
            Title = "Select the file to open",
            FileTypeFilter = [
                new FilePickerFileType("Modding project manifest") {
                    Patterns = ["*.modproj", "*.mdp", "*.yml"],
                }
            ],
        };

        IReadOnlyList<IStorageFile> files = await TopLevel.GetTopLevel(this)!
            .StorageProvider
            .OpenFilePickerAsync(options)
            .ConfigureAwait(false);

        return files.Count > 0 ? files[0] : null;
    }
}
