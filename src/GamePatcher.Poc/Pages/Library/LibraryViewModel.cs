namespace PleOps.GamePatcher.Poc.Pages.Library;

using System.Collections.ObjectModel;

internal class LibraryViewModel : ViewModelBase, IStackViewModel
{
    public LibraryViewModel()
    {
        Catalog = [];
    }

    public string ViewName => "Library";

    public ObservableCollection<LibraryProjectCardViewModel> Catalog { get; }
}
