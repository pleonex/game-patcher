namespace PleOps.GamePatcher.Poc.Pages.Library;

using System;
using System.Collections.Generic;
using System.Linq;
using PleOps.GamePatcher.Poc.ModdingProject;

public partial class LibraryProjectCardViewModel : ViewModelBase
{
    public LibraryProjectCardViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);

        Manifest = manifest;
        ProjectInfo = manifest.Project;
        Platforms = manifest.Products.Select(p => p.Platform).Distinct();
        Languages = manifest.Mods.Select(m => m.TargetLanguage).Distinct();
    }

    public ModdingProjectManifest Manifest { get; }

    public Project ProjectInfo { get; }

    public IEnumerable<string> Platforms { get; }

    public IEnumerable<string> Languages { get; }
}
