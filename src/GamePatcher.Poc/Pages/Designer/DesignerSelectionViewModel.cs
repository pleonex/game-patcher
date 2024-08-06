namespace PleOps.GamePatcher.Poc.Pages.Designer;

internal class DesignerSelectionViewModel : ViewModelBase, IStackViewModel
{
    private readonly AppNavigator navigator;

    public DesignerSelectionViewModel()
    {
        navigator = AppNavigator.Instance;
    }

    public string ViewName => "Designer";

    public AppNavigator Navigator => navigator;
}
