namespace PleOps.GamePatcher.Poc.Pages.Installer;

using CommunityToolkit.Mvvm.ComponentModel;

public abstract partial class ModInstallerStepViewModelBase : ViewModelBase
{
    [ObservableProperty]
    private bool canContinue;

    [ObservableProperty]
    private bool canGoBack;
}
