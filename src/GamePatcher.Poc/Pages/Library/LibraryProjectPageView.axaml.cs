namespace PleOps.GamePatcher.Poc.Pages.Library;

using Avalonia.Controls;

public partial class LibraryProjectPageView : UserControl
{
    public LibraryProjectPageView()
    {
        InitializeComponent();
    }

    private void CarouselPreviousClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        screenshotsCarousel.Previous();
    }

    private void CarouselNextClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        screenshotsCarousel.Next();
    }
}

internal sealed class LibraryProjectPageViewModelDesigner : LibraryProjectPageViewModel
{
    public LibraryProjectPageViewModelDesigner()
        : base(DesignerDataProvider.GetExampleModdingProjectManifest())
    {
    }
}
