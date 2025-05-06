namespace PleOps.GamePatcher.Poc.Pages.ModdingProject;

using System;
using PleOps.Moxmi.ModdingProject;

internal class ModdingProjectInfoEditorViewModel : ViewModelBase
{
    public ModdingProjectInfoEditorViewModel(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);
        Project = project;
    }

    public Project Project { get; }
}
