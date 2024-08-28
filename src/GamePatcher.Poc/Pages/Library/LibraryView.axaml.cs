namespace PleOps.GamePatcher.Poc.Pages.Library;

using Avalonia.Controls;

public partial class LibraryView : UserControl
{
    public LibraryView()
    {
        InitializeComponent();
    }

    private void CatalogDoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender is ListBox listBox && listBox.SelectedItem is LibraryProjectCardViewModel selectedViewModel) {
            AppNavigator.Instance.NavigateToLibraryProject(selectedViewModel.Manifest);
        }
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
