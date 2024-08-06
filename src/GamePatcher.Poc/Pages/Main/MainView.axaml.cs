namespace PleOps.GamePatcher.Poc.Pages.Main;

using Avalonia.Controls;
using FluentAvalonia.UI.Controls;

public partial class MainView : UserControl
{
    private readonly MainViewModel viewModel;

    public MainView()
    {
        InitializeComponent();
        AppNavigator.Instance.Initialize(mainNavigationFrame);

        viewModel = new MainViewModel();
        DataContext = viewModel;

        mainNavigationView.BackRequested += OnNavigateBackButtonPressed;
        mainNavigationView.SelectionChanged += OnMainNavigationItemChange;
        mainNavigationView.SelectedItem = mainNavigationView.MenuItems[0];
    }

    private void OnNavigateBackButtonPressed(object? sender, NavigationViewBackRequestedEventArgs e)
    {
        viewModel.NavigateBack();
    }

    private void OnMainNavigationItemChange(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is not NavigationViewItem nvi) {
            return;
        }

        viewModel.NavigateToPage(nvi.Tag as string);
    }
}
