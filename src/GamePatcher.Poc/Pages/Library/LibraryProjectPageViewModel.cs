namespace PleOps.GamePatcher.Poc.Pages.Library;

using System;
using System.Collections.Generic;
using System.Linq;
using PleOps.Moxmi.ModdingProject;

internal partial class LibraryProjectPageViewModel : ViewModelBase, IStackViewModel
{
    private readonly ModdingProjectManifest manifest;

    public LibraryProjectPageViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        this.manifest = manifest;

        ViewName = "Project info";
        ProjectInfo = manifest.Project;
        Images = [
            manifest.Project.Logo.Large?.Source,
            .. manifest.Mods.SelectMany(m => m.Screenshots.Select(s => s.Source))];
        Products = manifest.Products;
    }

    public string ViewName { get; }

    public Project ProjectInfo { get; }

    public IReadOnlyCollection<string> Images { get; }

    public IReadOnlyCollection<Product> Products { get; }
}
