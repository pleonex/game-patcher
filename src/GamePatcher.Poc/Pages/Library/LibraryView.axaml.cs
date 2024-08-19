namespace PleOps.GamePatcher.Poc.Pages.Library;

using Avalonia.Controls;

public partial class LibraryView : UserControl
{
    public LibraryView()
    {
        InitializeComponent();
    }
}

internal sealed class LibraryViewModelDesigner : LibraryViewModel
{
    public LibraryViewModelDesigner()
    {
        var testProject = DesignerDataProvider.GetExampleModdingProjectManifest();
        Catalog.Add(new LibraryProjectCardViewModel(testProject));
        Catalog.Add(new LibraryProjectCardViewModel(testProject));
        Catalog.Add(new LibraryProjectCardViewModel(testProject));
        Catalog.Add(new LibraryProjectCardViewModel(testProject));
    }
}
