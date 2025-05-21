namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System.Diagnostics.CodeAnalysis;
using FluentAvalonia.UI.Controls;

public class ModInstallerStepInfo
{
    public ModInstallerStepInfo()
    {
    }

    [SetsRequiredMembers]
    public ModInstallerStepInfo(int number, string stepName, Symbol icon)
    {
        Number = number;
        StepName = stepName;
        StepIcon = icon;
    }

    public required int Number { get; init; }

    public required string StepName { get; init; }

    public required Symbol StepIcon { get; init; }

    public ViewModelBase? ViewModel { get; set; }

    public bool IsEnabled { get; set; }
}
