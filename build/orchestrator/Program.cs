using Cake.Core;
using Cake.Frosting;
using Cake.Frosting.PleOps.Recipe;
using Cake.Frosting.PleOps.Recipe.Dotnet;

return new CakeHost()
    .AddAssembly(typeof(Cake.Frosting.PleOps.Recipe.PleOpsBuildContext).Assembly)
    .UseContext<PleOpsBuildContext>()
    .UseLifetime<BuildLifetime>()
    .Run(args);

public sealed class BuildLifetime : FrostingLifetime<PleOpsBuildContext>
{
    public override void Setup(PleOpsBuildContext context, ISetupContext info)
    {
        context.ReadArguments();

        context.Print();
    }

    public override void Teardown(PleOpsBuildContext context, ITeardownContext info)
    {
        // Save the info from the existing artifacts for the next execution (e.g. deploy job)
        context.DeliveriesContext.Save();
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.SetGitVersionTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.CleanArtifactsTask))]
public sealed class DefaultTask : FrostingTask
{
}

[TaskName("Bundle")]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.SetGitVersionTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.GitHub.ExportReleaseNotesTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.DocFx.BuildTask))]
public sealed class BundleTask : FrostingTask
{
}

[TaskName("Deploy")]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.SetGitVersionTask))]
public sealed class DeployTask : FrostingTask
{
}
