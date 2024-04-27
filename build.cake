
///////////////////////////////////////////////////////////////////////////////
/// TOOLS & ADDINS
///////////////////////////////////////////////////////////////////////////////
#addin nuget:?package=Cake.Git&version=4.0.0

///////////////////////////////////////////////////////////////////////////////
/// USINGS & NAMESPACES
///////////////////////////////////////////////////////////////////////////////
#r "Spectre.Console"
using Spectre.Console

///////////////////////////////////////////////////////////////////////////////
/// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
GitVersion gitVersion;
var repoName = "gitsandbox";

// Arguments
var target   = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
/// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(ctx => {
    // https://gitversion.net/docs/usage/cli/arguments
    // https://cakebuild.net/api/Cake.Core.Tooling/ToolSettings/50AAB3A8
    gitVersion = GitVersion(new GitVersionSettings 
    { 
        OutputType            = GitVersionOutput.Json,
        Verbosity             = GitVersionVerbosity.Verbose,        
        ArgumentCustomization = args => args.Append("/updateprojectfiles")
    });
    var branchName = gitVersion.BranchName;
    var workingDirectory = System.IO.Directory.GetCurrentDirectory();

    AnsiConsole.Write(
        new FigletText($"{repoName}")
            .LeftJustified()
            .Color(Color.Red));

    Information("Working directory         : {0}", workingDirectory);  
    Information("Branch                    : {0}", branchName);
    Information("Informational      Version: {0}", gitVersion.InformationalVersion);
    Information("SemVer             Version: {0}", gitVersion.SemVer);
    Information("AssemblySemVer     Version: {0}", gitVersion.AssemblySemVer);
    Information("AssemblySemFileVer Version: {0}", gitVersion.AssemblySemFileVer);
    Information("MajorMinorPatch    Version: {0}", gitVersion.MajorMinorPatch);
    Information("NuGet              Version: {0}", gitVersion.NuGetVersion);
});

///////////////////////////////////////////////////////////////////////////////
/// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("info").Does(()=> { 
        /*Does nothing but specifying information of the build*/ 
});

///////////////////////////////////////////////////////////////////////////////
/// DEPENDENCIES
///////////////////////////////////////////////////////////////////////////////
Task("default").IsDependentOn("info");

RunTarget(target);