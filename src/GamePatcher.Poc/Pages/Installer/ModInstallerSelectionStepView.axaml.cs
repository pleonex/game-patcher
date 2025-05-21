namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;

public partial class ModInstallerSelectionStepView : UserControl
{
    private readonly TaskDialog errorReadingDialog;

    public ModInstallerSelectionStepView()
    {
        InitializeComponent();

        errorReadingDialog = new TaskDialog() {
            Header = "Error reading MIX package",
            IconSource = new SymbolIconSource {
                Symbol = (Symbol)0xE7BA, // warning symbol
            },
            SubHeader = "There was an issue reading the MIX package.",
            Buttons = [new TaskDialogButton("Close", TaskDialogStandardResult.Close)],
        };
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is ModInstallerSelectionStepViewModel viewModel) {
            viewModel.AskOpenFile.RegisterHandler(SelectOpenFileAsync);
            viewModel.DisplayMixReadError.RegisterHandler(DisplayMixReadErrorAsync);
        }

        base.OnDataContextChanged(e);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        errorReadingDialog.XamlRoot = VisualRoot as Visual;
    }

    private async Task<IStorageFile?> SelectOpenFileAsync()
    {
        var options = new FilePickerOpenOptions {
            AllowMultiple = false,
            Title = "Select the MIX mod package",
            FileTypeFilter = [
                new FilePickerFileType("Mod installer extensible package") {
                    Patterns = ["*.mix"],
                },
                FilePickerFileTypes.All,
            ],
        };

        IReadOnlyList<IStorageFile> files = await TopLevel.GetTopLevel(this)!
            .StorageProvider
            .OpenFilePickerAsync(options)
            .ConfigureAwait(false);

        return files.Count > 0 ? files[0] : null;
    }

    private async Task<object?> DisplayMixReadErrorAsync(Exception ex)
    {
        return await Dispatcher.UIThread.InvokeAsync(async () => {
            errorReadingDialog.Content = ex.Message;

            // windows mode false by setting showHosted to true
            return await errorReadingDialog.ShowAsync(showHosted: true).ConfigureAwait(false);
        });
    }

    private void OnControlDoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (DataContext is not ModInstallerSelectionStepViewModel viewModel) {
            return;
        }

        viewModel.SelectMixPackageCommand.Execute(null);
    }
}
