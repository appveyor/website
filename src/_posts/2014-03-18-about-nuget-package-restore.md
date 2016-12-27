---
title: About NuGet package restore
---

NuGet provides few different methods of restoring missing NuGet packages
during application development on in Continuous Integration environment.
In this article we'll review these methods to find out which one to choose
for using in AppVeyor CI environment.

After reading the original
[NuGet Package Restore article](https://docs.nuget.org/consume/package-restore)
on NuGet docs web site we see that today we have three options for restoring packages:

## Restoring packages as part of MSBuild process

Important note here - not before, but **during** MSBuild process.
This is accomplished by injecting nuget.targets into build pipeline,
so you end up with .nuget folder in your solution.
This method works in build environment and requires consent which
is EnableNuGetPackageRestore environment variable
(it's ON by default starting from NuGet 2.7 but we set it in AppVeyor
environment for compatibility with previous NuGet versions).
Starting from NuGet 2.7 this method is considered as obsolete
as it requires additional folder with nuget.exe, nuget.targets
and do not work in some scenarios (remember that chicken/egg
[problem with BCL packages](https://blogs.msdn.microsoft.com/dotnet/2013/08/22/improved-package-restore/).

## Automatic package restore in Visual Studio

This method is part of NuGet Visual Studio add-in (.vsix),
heavily relies on VS events and works in interactive mode, not build environment.
Excerpt from that page:

![NuGet restore excerpt](/assets/img/posts/nuget-restore/nuget-restore-excerpt.png)

In other words, restore occurs in Visual Studio and **before** MSBuild process.

## Command-line package restore

The method was always there, but "was improved in NuGet 2.7".
This is exactly what we need and do in AppVeyor build environment!
All that you need is to put "nuget restore" command into "Install scripts"
or "Before build scripts" box of your project settings:

![before-build-nuget-restore](/assets/img/posts/nuget-restore/before-build-nuget-restore.png)

or in appveyor.yml:

```yaml
before_build:
  - nuget restore
```

Hope that helps.
