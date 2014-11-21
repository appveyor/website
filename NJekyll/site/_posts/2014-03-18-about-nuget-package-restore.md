---
layout: post
title: About NuGet package restore
---

NuGet provides few different methods of restoring missing NuGet packages during application development on in Continuous Integration environment. In this article we'll review these methods to find out which one to choose for using in AppVeyor CI environment.

After reading original <a href="http://docs.nuget.org/docs/reference/package-restore">NuGet Package Restore article</a> on NuGet docs web site we see that today we have three options for restoring packages:
<h2>Restoring packages as part of MSBuild process</h2>
Iimportant note here - not before, but <b>during </b>MSBuild process. This is accomplished by injecting nuget.targets into build pipeline, so you end up with .nuget folder in your solution. This method works in build environment and requires consent which is EnableNuGetPackageRestore environment variable (it's ON by default starting from NuGet 2.7 but we set it in AppVeyor environment for compatibility with previous NuGet versions). Starting from NuGet 2.7 this method is considered as obsolete as it requires additional folder with nuget.exe, nuget.targets and do not work in some scenarios (remember that chicken/egg <a href="http://blogs.msdn.com/b/dotnet/archive/2013/08/22/improved-package-restore.aspx" target="_blank">problem with BCL packages</a>).
<h2>Automatic package restore in Visual Studio</h2>
This method is part of NuGet Visual Studio add-in (.vsix), heavily relies on VS events and works in interactive mode, not build environment. Excerpt from that page:

<img class="alignnone size-full wp-image-264" alt="nuget-restore-excerpt" src="/site/_posts/images/nuget-restore/nuget-restore-excerpt.png" />

Other words, restore occurs in Visual Studio and <b>before </b>MSBuild process.
<h2>Command-line package restore</h2>
<span style="font-style:inherit;line-height:1.625;">The method was always there, but "was improved in NuGet 2.7". This is exactly what we need and do in AppVeyor build environment! All that you need is to put "nuget restore" command into "Install scripts" or "Before build scripts" box of your project settings:</span>

<img class="alignnone size-full wp-image-265" alt="before-build-nuget-restore" src="/site/_posts/images/nuget-restore/before-build-nuget-restore.png" />

or in appveyor.yml:
<pre>before_build:
  - nuget restore</pre>
<span style="font-style:inherit;line-height:1.625;">Hope that helps.</span>