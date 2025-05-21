namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System;
using Avalonia;
using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;

public partial class ModInstallerLayoutView : UserControl
{
    public ModInstallerLayoutView()
    {
        InitializeComponent();

        installerFrame.NavigationPageFactory = new ViewLocator();
        installerNavigation.SelectionChanged += OnNavigationSelectionChanged;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        installerNavigation.SelectedItem = installerNavigation.MenuItemsSource.ElementAt(0);
        base.OnAttachedToVisualTree(e);
    }

    private void OnNavigationSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is not ModInstallerStepInfo stepInfo) {
            return;
        }

        installerFrame.NavigateFromObject(stepInfo.ViewModel);
    }
}
