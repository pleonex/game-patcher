namespace PleOps.GamePatcher.Poc.Pages.Library;

using Avalonia.Controls;
using PleOps.GamePatcher.Poc.Pages;

public partial class LibraryProjectCardView : UserControl
{
    public LibraryProjectCardView()
    {
        InitializeComponent();
    }
}

internal class LibraryProjectCardViewModelDesigner : LibraryProjectCardViewModel
{
    public LibraryProjectCardViewModelDesigner()
        : base(DesignerDataProvider.GetExampleModdingProjectManifest())
    {
    }
}
