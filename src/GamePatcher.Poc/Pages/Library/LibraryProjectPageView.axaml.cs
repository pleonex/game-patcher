namespace PleOps.GamePatcher.Poc.Pages.Library;

using Avalonia.Controls;

public partial class LibraryProjectPageView : UserControl
{
    public LibraryProjectPageView()
    {
        InitializeComponent();
    }
}

internal sealed class LibraryProjectPageViewModelDesigner : LibraryProjectPageViewModel
{
    public LibraryProjectPageViewModelDesigner()
        : base(DesignerDataProvider.GetExampleModdingProjectManifest())
    {
    }
}
