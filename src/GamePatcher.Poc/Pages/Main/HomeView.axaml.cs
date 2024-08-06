namespace PleOps.GamePatcher.Poc.Pages.Main;

using Avalonia.Controls;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        DataContext = new HomeViewModel();
    }
}
