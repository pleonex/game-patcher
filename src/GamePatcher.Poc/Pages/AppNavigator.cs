namespace PleOps.GamePatcher.Poc.Pages;

using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using PleOps.GamePatcher.Poc.Pages.Designer;
using PleOps.GamePatcher.Poc.Pages.Library;

internal partial class AppNavigator : ObservableObject
{
    private static readonly object instanceLock = new object();
    private static AppNavigator? instance;

    private readonly ViewLocator viewLocator;
    private Frame? frame;

    [ObservableProperty]
    private string pageTitle = string.Empty;

    private AppNavigator()
    {
        viewLocator = new ViewLocator();
    }

    public static AppNavigator Instance {
        get {
            if (instance is null) {
                lock (instanceLock) {
                    instance ??= new AppNavigator();
                }
            }

            return instance;
        }
    }

    public bool CanNavigateBack => frame?.BackStackDepth > 0;

    public ObservableCollection<string> NavigationSegments { get; } = new();

    public void Initialize(Frame frame)
    {
        this.frame = frame;
        frame.NavigationPageFactory = viewLocator;
    }

    [RelayCommand]
    public void NavigateBack()
    {
        if (frame is null) {
            throw new InvalidOperationException("You forgot the initialize");
        }

        if (!CanNavigateBack) {
            return;
        }

        frame.GoBack();
        NavigationSegments?.RemoveAt(NavigationSegments.Count - 1);

        OnPropertyChanged(nameof(CanNavigateBack));
    }

    [RelayCommand]
    public void NavigateToLibrary()
    {
        NavigateTo<LibraryViewModel>(true);
    }

    [RelayCommand]
    public void NavigateToDesignerSelection()
    {
        NavigateTo<DesignerSelectionViewModel>(true);
    }

    [RelayCommand]
    public void NavigateToModdingProjectEditor()
    {
        NavigateTo<ModdingProjectEditorViewModel>(false);
    }

    [RelayCommand]
    public void NavigateToModInstallerEditor()
    {
        NavigateTo<ModInstallerEditorViewModel>(false);
    }

    private void NavigateTo<T>(bool rootNavigation)
        where T: IStackViewModel, new()
    {
        if (frame is null) {
            throw new InvalidOperationException("You forgot the initialize");
        }

        T viewModel = new T(); // TODO: use DI
        PageTitle = viewModel.ViewName;

        frame.NavigateFromObject(viewModel);

        if (rootNavigation) {
            frame.BackStack.Clear();
            NavigationSegments.Clear();
        }

        NavigationSegments.Add(viewModel.ViewName);
        OnPropertyChanged(nameof(CanNavigateBack));
    }
}
