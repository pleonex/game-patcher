namespace PleOps.GamePatcher.Poc.Pages.ModdingProject;

using Avalonia.Controls;

public partial class ModdingProjectInfoEditorView : UserControl
{
    public ModdingProjectInfoEditorView()
    {
        InitializeComponent();
    }
}

internal class ModdingProjectInfoEditorViewModelDesigner : ModdingProjectInfoEditorViewModel
{
    public ModdingProjectInfoEditorViewModelDesigner()
        : base(DesignerDataProvider.GetExampleModdingProjectManifest().Project)
    {
    }
}
