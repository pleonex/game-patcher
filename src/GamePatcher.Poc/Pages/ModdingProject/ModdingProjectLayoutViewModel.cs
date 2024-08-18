namespace PleOps.GamePatcher.Poc.Pages.ModdingProject;

using System;
using System.Collections.ObjectModel;
using PleOps.GamePatcher.Poc.ModdingProject;

internal class ModdingProjectLayoutViewModel : ViewModelBase, IStackViewModel
{
    public ModdingProjectLayoutViewModel(ModdingProjectManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        Project = new ModdingProjectInfoEditorViewModel(manifest.Project);
        Products = new ObservableCollection<Product>(manifest.Products);
        Mods = new ObservableCollection<ModInfo>(manifest.Mods);
    }

    public string ViewName => "Modding project editor";

    public ModdingProjectInfoEditorViewModel Project { get; }

    public ObservableCollection<Product> Products { get; }

    public ObservableCollection<ModInfo> Mods { get; }
}
