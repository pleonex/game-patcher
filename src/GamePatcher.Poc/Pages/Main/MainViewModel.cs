namespace PleOps.GamePatcher.Poc.Pages.Main;

using System.Collections.ObjectModel;
using System.ComponentModel;

internal partial class MainViewModel : ViewModelBase
{
    private readonly AppNavigator navigator;

    public MainViewModel()
    {
        navigator = AppNavigator.Instance;
        navigator.PropertyChanged += OnNavigatorPropertyChanged;
    }

    public ObservableCollection<string> NavigationSegments => navigator.NavigationSegments;

    public bool CanNavigateBack => navigator.CanNavigateBack;

    public void NavigateBack()
    {
        navigator.NavigateBack();
    }

    public void NavigateToPage(string? pageName)
    {
        switch (pageName) {
            case "Library":
                AppNavigator.Instance.NavigateToLibrary();
                break;
            case "Designer":
                AppNavigator.Instance.NavigateToDesignerSelection();
                break;
        }
    }

    private void OnNavigatorPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AppNavigator.CanNavigateBack)) {
            OnPropertyChanged(nameof(CanNavigateBack));
        }
    }
}
