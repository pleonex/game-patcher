namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PleOps.GamePatcher.Poc.Mvvm;
using PleOps.Moxmi.ModInstallerExtensible;

public partial class ModInstallerSelectionStepViewModel : ModInstallerStepViewModelBase
{
    [ObservableProperty]
    private MixPackage? inputMix;

    public ModInstallerSelectionStepViewModel()
    {
        CanContinue = false;
        CanGoBack = false;

        AskOpenFile = new AsyncInteraction<IStorageFile?>();
        DisplayMixReadError = new AsyncInteraction<Exception, object?>();
    }

    public AsyncInteraction<IStorageFile?> AskOpenFile { get; }

    public AsyncInteraction<Exception, object?> DisplayMixReadError { get; }


    [RelayCommand]
    private async Task SelectMixPackageAsync()
    {
        IStorageFile? selectedFile = await AskOpenFile.HandleAsync();
        if (selectedFile is null) {
            return;
        }

        await ReadMixPackageAsync(selectedFile);
    }

    [RelayCommand]
    private async Task ReadMixPackageAsync(IStorageFile file)
    {
        string? path = file.TryGetLocalPath();
        if (string.IsNullOrEmpty(path)) {
            // TODO: log
            return;
        }

        try {
            InputMix = MixPackage.FromZipFile(path);
            CanContinue = true;
        } catch (Exception ex) {
            // TODO: log
            _ = await DisplayMixReadError.HandleAsync(ex);
        }
    }
}
