namespace PleOps.GamePatcher.Poc.Pages.Library;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PleOps.GamePatcher.Poc.ModdingProject;

internal partial class LibraryProjectPageViewModel : ViewModelBase, IStackViewModel
{
    private readonly ModdingProjectManifest manifest;

    public LibraryProjectPageViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        this.manifest = manifest;

        ViewName = "Project info";
        ProjectInfo = manifest.Project;
        Images = [manifest.Project.Logo.Large, .. manifest.Mods.SelectMany(m => m.Screenshots.Select(s => s.Href))];
        Products = manifest.Products;
    }

    public string ViewName { get; }

    public Project ProjectInfo { get; }

    public IReadOnlyCollection<string> Images { get; }

    public IReadOnlyCollection<Product> Products { get; }
}
