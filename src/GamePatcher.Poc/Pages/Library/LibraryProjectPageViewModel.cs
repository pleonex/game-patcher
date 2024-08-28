namespace PleOps.GamePatcher.Poc.Pages.Library;

using System;
using PleOps.GamePatcher.Poc.ModdingProject;

internal partial class LibraryProjectPageViewModel : ViewModelBase, IStackViewModel
{
    private readonly ModdingProjectManifest manifest;

    public LibraryProjectPageViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        this.manifest = manifest;

        ViewName = manifest.Project.Name;
        ProjectInfo = manifest.Project;
    }

    public string ViewName { get; }

    public Project ProjectInfo { get; }
}
