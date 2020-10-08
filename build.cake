#load "nuget:?package=PleOps.Cake&prerelease"

Task("Define-Project")
    .Description("Fill specific project information")
    .Does<BuildInfo>(info =>
{
    info.WarningsAsErrors = false;
    info.AddLibraryProjects("GamePatcher");
    info.AddLibraryProjects("GamePatcher.UI");
    info.AddApplicationProjects("GamePatcher.UI.Gtk");
    info.AddTestProjects("GamePatcher.Tests");
});

Task("Default")
    .IsDependentOn("Stage-Artifacts");

string target = Argument("target", "Default");
RunTarget(target);
