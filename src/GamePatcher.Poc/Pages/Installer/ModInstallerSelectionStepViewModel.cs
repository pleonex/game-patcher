namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PleOps.GamePatcher.Poc.Mvvm;
using PleOps.Moxmi.ModInstallerExtensible;

public partial class ModInstallerSelectionStepViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool canContinue;

    [ObservableProperty]
    private bool canGoBack;

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

        try {
            InputMix = MixPackage.FromZipFile(selectedFile.Path.AbsolutePath);
            CanContinue = true;
        } catch (Exception ex) {
            // TODO: log
            _ = await DisplayMixReadError.HandleAsync(ex);
        }
    }
}
