namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;

public class ModInstallerLayoutViewModel : ViewModelBase, IStackViewModel
{
    public ModInstallerLayoutViewModel()
    {
        Steps = new List<ModInstallerStepInfo>() {
            new(1, "Mod info", Symbol.ZipFolder) { ViewModel = new ModInstallerSelectionStepViewModel() },
            new(2, "Game compatibility", Symbol.Checkmark),
            new(3, "Hardware compatibility", Symbol.XboxConsole),
            new(4, "Parameters", Symbol.Setting),
            new(5, "Installation", Symbol.Repair),
            new(6, "Bundle", Symbol.FolderFilled),
        }.AsReadOnly();
    }

    public string ViewName => "!!POC!! MOXMI Universal Patcher";

    public ReadOnlyCollection<ModInstallerStepInfo> Steps { get; }
}
