namespace PleOps.GamePatcher.Poc.Pages.Designer;

using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using PleOps.GamePatcher.Poc.ModdingProject;
using PleOps.GamePatcher.Poc.Mvvm;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using Avalonia.Threading;

internal partial class DesignerSelectionViewModel : ViewModelBase, IStackViewModel
{
    private readonly AppNavigator navigator;

    public DesignerSelectionViewModel()
    {
        navigator = AppNavigator.Instance;
        AskOpenFile = new AsyncInteraction<IStorageFile?>();
    }

    public string ViewName => "Designer";

    public AppNavigator Navigator => navigator;

    public AsyncInteraction<IStorageFile?> AskOpenFile { get; }

    [RelayCommand]
    private async Task OpenFileAsync()
    {
        IStorageFile? selectedFile = await AskOpenFile.HandleAsync().ConfigureAwait(false);
        if (selectedFile is null) {
            return;
        }

        using Stream fileStream = await selectedFile.OpenReadAsync();
        using var reader = new StreamReader(fileStream);
        string manifestContent = reader.ReadToEnd();

        var manifest = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<ModdingProjectManifest>(manifestContent);

        await Dispatcher.UIThread.InvokeAsync(() =>
            navigator.NavigateToModdingProjectEditor(manifest));
    }
}
