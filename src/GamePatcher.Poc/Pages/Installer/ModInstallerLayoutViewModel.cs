namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;

public partial class ModInstallerLayoutViewModel : ViewModelBase, IStackViewModel
{
    [ObservableProperty]
    private ModInstallerStepViewModelBase currentStep;

    [ObservableProperty]
    private int currentStepIndex;

    [ObservableProperty]
    private ModInstallerStepInfo currentStepInfo;

    public ModInstallerLayoutViewModel()
    {
        Steps = new List<ModInstallerStepInfo>() {
            new(1, "Mod info", Symbol.ZipFolder),
            new(2, "Game compatibility", Symbol.Checkmark),
            new(3, "Hardware compatibility", Symbol.XboxConsole),
            new(4, "Parameters", Symbol.Setting),
            new(5, "Installation", Symbol.Repair),
            new(6, "Bundle", Symbol.FolderFilled),
        }.AsReadOnly();

        CurrentStepIndex = 0;
        CurrentStepInfo = Steps[0];
        CurrentStep = new ModInstallerSelectionStepViewModel();
    }

    public string ViewName => "!!POC!! MOXMI Universal Patcher";

    public ReadOnlyCollection<ModInstallerStepInfo> Steps { get; }

    partial void OnCurrentStepChanged(ModInstallerStepViewModelBase? oldValue, ModInstallerStepViewModelBase newValue)
    {
        if (oldValue is not null) {
            oldValue.PropertyChanged -= OnCurrentStepPropertyChanged;
        }

        newValue.PropertyChanged += OnCurrentStepPropertyChanged;
    }

    private void OnCurrentStepPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ModInstallerStepViewModelBase.CanContinue)) {
            ContinueStepCommand.NotifyCanExecuteChanged();
        } else if (e.PropertyName == nameof(ModInstallerStepViewModelBase.CanGoBack)) {
            GoBackStepCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanContinueStep))]
    private void ContinueStep()
    {
        CurrentStepIndex++;
        CurrentStepInfo = Steps[CurrentStepIndex];
    }

    private bool CanContinueStep()
    {
        return CurrentStep.CanContinue;
    }

    [RelayCommand(CanExecute = nameof(CanGoBackStep))]
    private void GoBackStep()
    {
        CurrentStepIndex--;
        CurrentStepInfo = Steps[CurrentStepIndex];
    }

    private bool CanGoBackStep()
    {
        return CurrentStep.CanGoBack;
    }
}
