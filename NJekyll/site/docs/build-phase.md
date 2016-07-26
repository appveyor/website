---
layout: docs
title: Build phase
---

# Build phase

After cloning the repository AppVeyor runs **MSBuild** to build project sources and package artifacts.

The following command is used:

    msbuild <project> /logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"

> Custom logger is required to push MSBuild warning and errors in real-time to build console. You may use this logger in your own build scripts.

`<project>` is a Visual Studio project (`*.*proj`) or solution (`*.sln`) file. If project or solution file is not specified on project settings AppVeyor searches for the first occurence of `*.sln` or `*.*proj` file in the build clone directory recursively.

You may configure your own custom build script ("Script" mode) instead of calling MSBuild or disable build phase altogether.


## Automatic packaging of build artifacts

Before running MSBuild AppVeyor analyzes the supplied project or solution file to determine the type and "flavor" of all projects.

### Packaging Web Application projects (WAP)

To package Visual Studio WAP AppVeyor runs the following command:

    msbuild <wap_project> /t:Package /p:PackageLocation=<web-deploy-package.zip>

This command performs compilation and publishing of WAP into a Web Deploy package. The produced package is pushed to artifacts and can be deployed using [Web Deploy](/docs/deployment/web-deploy) or [Agent](/docs/deployment/agent) providers.


### Packaging NuGet libraries

If the project folder contains a `*.nuspec` file AppVeyor will try to package the project as a NuGet library by calling the following command:

    nuget pack <project_file> -OutputDirectory <temp_path>

For example, given the following project structure:

![nuget-pack-project](/site/images/docs/nuget-pack-project.png)

AppVeyor will call:

    nuget pack SimpleConsole.Tests.csproj -OutputDirectory <temp_path>

For AppVeyor to *find* the `.nuspec` file, it needs to be:
1. The same name as the `.csprj` file. For example (refer to the above image, again)
 - project file: `SimpleConsole.Tests.csproj`  
 - nuspec file: `SimpleConsole.Tests.nuspec`
2. Side by side to the `.csproj` file. So the same folder, etc.

If those two conditions are not met, then the nuget packaging will not work.  

>If you have multiple `.nuspec's` then you cannot use this built in flow and will need to create your own powershell scripts and manually pack/deploy in another step, like the `build_success` step, for example.

To pass the current AppVeyor build version into the generated nuget package (a `.nupkg`) you need to do two things:
1. Add the `[assembly: AssemblyInformationalVersionAttribute("1.0")]` to the Visual Studio project's `AssemblyInfo.cs` file.
eg.

```
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersionAttribute("1.0")]
```
2. Modify the `<version>` element in the `.nuspec` file to: `<version>$version$</version>` (the `$version` will be replaced by AppVeyor during the NuGet packaging stage)
3. Update your `appveyor.yml` file to tell it it [AssemblyInfo Patch](http://www.appveyor.com/docs/build-configuration#assemblyinfo-patching) the `AssemblyInfo.cs` file ... specifically, the new `AssemblyInformationalVersionAttribute` you've just added. 
```
assembly_info:
  patch: true
  file: AssemblyInfo.*
  assembly_version: $(appveyor_build_version)
  assembly_file_version: $(appveyor_build_version)
  assembly_informational_version: $(appveyor_build_version)
```

So why do this / what is happening? NuGet uses the `ProductVersion` value of the dll ... which AppVeyor sets with the `assembly_informational_version` value of the `AssemblyInfo.cs`. 

Example references: 
- [Sample Project](https://github.com/FeodorFitsner/nuget-test)
- [Sample AssemblyInfo.cs](https://github.com/FeodorFitsner/nuget-test/blob/master/MyNuGetLib/Properties/AssemblyInfo.cs)
- [Sample .nuspec](https://github.com/FeodorFitsner/nuget-test/blob/master/MyNuGetLib/MyNuGetLib.nuspec)
- [Sample appveyor.yml](https://github.com/FeodorFitsner/nuget-test/blob/master/appveyor.yml)

> To generate a `.nuspec` file for your project use the `nuget spec` command or use [NuGet Package Explorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer) to easily generate one manually.



### Packaging Azure Cloud Service projects

If Azure Cloud Service project (`.ccproj`) is found with the solution AppVeyor will create Azure Cloud Service package (`.cspkg`) using the following command:

    msbuild <ccproj_file> /t:Publish /p:PublishDir=<temp_path>

Created Cloud Service package (`<project-name>.cspkg`) and default "Cloud" configuration (`<project-name>.cscfg`) will be published to build artifacts. In addition to that **all** `.cscfg` files found in Cloud Service project folder are pushed to artifacts with names `<project-name>.<config>.cscfg`.

#### Selecting cloud service configuration

By default, MSBuild uses `<project>.Cloud.cscfg` cloud service configuration, but you can specify which configuration to use by adding a `TargetProfile` environment variable, for example:

<table>
    <tr>
        <td>TargetProfile</td>
        <td>Staging</td>
    </tr>
</table>

#### Caveats

While trying to build an Azure Cloud Service project you may get the following (or similar related) error:

    The imported project "C:\Program Files (x86)\MSBuild\Microsoft\VisualStudio\vX.X\Windows Azure Tools\2.X\Microsoft.WindowsAzure.targets" was not found.

To fix this error add the following environment variable on the **Environment** tab of project settings:

<table>
    <tr>
        <td>VisualStudioVersion</td>
        <td>12.0</td>
    </tr>
</table>

where 12.0 is your version of Visual Studio (VS 2013 - 12.0, VS 2012 - 11.0, VS 2010 - 10.0).

See also:

* [appveyor.yml reference](/docs/appveyor-yml)
* [Pushing real-time compilation messages to build console](/docs/build-worker-api#add-compilation-message)
