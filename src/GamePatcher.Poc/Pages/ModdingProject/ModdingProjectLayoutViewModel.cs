namespace PleOps.GamePatcher.Poc.Pages.ModdingProject;

using System;
using PleOps.GamePatcher.Poc.ModdingProject;

internal class ModdingProjectLayoutViewModel : ViewModelBase, IStackViewModel
{
    public ModdingProjectLayoutViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        Project = new ModdingProjectInfoEditorViewModel(manifest.Project);
    }

    public string ViewName => "Modding project editor";

    public ModdingProjectInfoEditorViewModel Project { get; }
}
