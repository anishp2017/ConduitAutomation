//#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
// parameters
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var version = Argument("versionNumber", "0.0.0");

// constants
const string sourceDirectory = "./ConduitAutomation";
const string artifactsDirectory = "./artifacts";
const string publishDirectory = "./artifacts/publish";
const string testResultsDirectory = "./artifacts/testResults";

// tasks
Task("Clean")
    .Does(() =>
    {
        CleanDirectories($"{sourceDirectory}/**/bin");
        CleanDirectories($"{sourceDirectory}/**/obj");
        CleanDirectory(artifactsDirectory);
        CleanDirectory(publishDirectory);
    });

Task("Restore-NuGet-Packages")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild("./", new DotNetCoreBuildSettings
        {
            Configuration = configuration
        });
    });

Task("Run-Tests")
    .Does(() =>
    {
        var testProjects = GetFiles($"{sourceDirectory}/*.csproj");
        foreach(var testProject in testProjects)
        {
            DotNetCoreTest(testProject.FullPath, new DotNetCoreTestSettings
            {
                Configuration = configuration,
                NoBuild = true
            });
        }
    });

// task targets
Task("Default")
    // .IsDependentOn("Clean")
    // .IsDependentOn("Restore-NuGet-Packages")
    // .IsDependentOn("Build")
    .IsDependentOn("Run-Tests");

// execution
RunTarget(target);
