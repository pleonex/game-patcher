namespace PleOps.GamePatcher.Poc.Pages.Installer;

using System;
using System.ComponentModel;
using Avalonia.Controls;

public partial class ModInstallerLayoutView : UserControl
{
    public ModInstallerLayoutView()
    {
        InitializeComponent();

        installerFrame.NavigationPageFactory = new ViewLocator();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is not ModInstallerLayoutViewModel viewModel) {
            return;
        }

        viewModel.PropertyChanged += OnViewModelPropertyChanged;
        NavigateToStep(viewModel.CurrentStep);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not ModInstallerLayoutViewModel viewModel) {
            return;
        }

        if (e.PropertyName == nameof(ModInstallerLayoutViewModel.CurrentStep)) {
            NavigateToStep(viewModel.CurrentStep);
        }
    }

    private void NavigateToStep(ModInstallerStepViewModelBase stepViewModel)
    {
        installerFrame.NavigateFromObject(stepViewModel);
    }
}
