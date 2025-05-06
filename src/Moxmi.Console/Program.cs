using PleOps.Moxmi.Console.Installer;
using NLog;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(static configurator => {
    configurator.AddBranch("installer", static installer => {
        installer.SetDescription("Install a mod");
        //installer.AddCommand<InteractiveInstallerCommand>("interactive");
    });
});

int result = await app.RunAsync(args);
LogManager.Shutdown();

AnsiConsole.WriteLine();
if (result == 0) {
    AnsiConsole.MarkupLineInterpolated($"[bold green]Success![/]");
} else {
    AnsiConsole.MarkupLineInterpolated($"[bold red]Failure...[/] Error code: {result}");
}

return result;
