namespace PleOps.GamePatcher.Poc.Pages.Designer;

using System;
using PleOps.GamePatcher.Poc.ModdingProject;

internal class ModdingProjectEditorViewModel : ViewModelBase, IStackViewModel
{
    public ModdingProjectEditorViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        Manifest = manifest;
    }

    public string ViewName => "Modding project editor";

    public ModdingProjectManifest Manifest { get; }
}
