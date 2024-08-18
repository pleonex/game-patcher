﻿namespace PleOps.GamePatcher.Poc.Pages.ModdingProject;

using Avalonia.Controls;
using FluentAvalonia.UI.Controls;

public partial class ModdingProjectLayoutView : UserControl
{
    public ModdingProjectLayoutView()
    {
        InitializeComponent();

        mdpFrame.NavigationPageFactory = new ViewLocator();
        mdpNavigation.SelectionChanged += OnNavigationItemChanged;
        mdpNavigation.SelectedItem = mdpNavigation.MenuItems[0];
    }

    private void OnNavigationItemChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (DataContext is not ModdingProjectLayoutViewModel viewModel
            || e.SelectedItem is not NavigationViewItem nvi) {
            return;
        }                                   

        if (nvi.Tag is "Project") {
            mdpFrame.NavigateFromObject(viewModel.Project);
        }
    }
}

internal class ModdingProjectLayoutViewModelDesigner : ModdingProjectLayoutViewModel
{
    public ModdingProjectLayoutViewModelDesigner()
        : base(DesignerDataProvider.GetExampleModdingProjectManifest())
    {
    }
}
