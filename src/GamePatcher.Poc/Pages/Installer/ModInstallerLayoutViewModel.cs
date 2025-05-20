namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;

public class ModInstallerLayoutViewModel : ViewModelBase, IStackViewModel
{
    public ModInstallerLayoutViewModel()
    {
        Steps = new List<ModInstallerStepInfo>() {
            new(1, "MIX package selection", Symbol.ZipFolder),
            new(2, "Mod information", Symbol.Checkmark),
            new(3, "Game selection", Symbol.Games),
            new(4, "Game compatibility", Symbol.Zoom),
            new(5, "Hardware compatibility", Symbol.XboxConsole),
            new(6, "Parameters", Symbol.Setting),
            new(7, "Installation", Symbol.Repair),
            new(8, "Bundle", Symbol.FolderFilled),
        }.AsReadOnly();
    }

    public string ViewName => "Mod installer";

    public ReadOnlyCollection<ModInstallerStepInfo> Steps { get; }
}
