---
layout: docs
title: Publishing to NuGet feed
---

# Publishing to NuGet feed

NuGet deployment provider publishes artifacts of type **NuGet package** to remote NuGet feed.

## Provider settings

* **NuGet server URL** (`server`) - NuGet feed URL, e.g. https://nugetserver.com/nuget/feed. If server is not specified package will be pushed to NuGet.org.
* **API key** (`api_key`) - your API key
* **Symbol server URL** (`symbol_server`) - Publishing URL for symbol server, If server is not specified symbol package will be pushed to SymbolSource.org.
* **Do not publish symbol packages** (`skip_symbols`) - skip publishing of symbol packages.
* **Artifact(s)** (`artifact`) - artifact name or filename to push. If not specified all artifacts of type **NuGet package** will be pushed. This can be a regexp, e.g. `/.*\.nupkg/`

Configuring in `appveyor.yml`:

    deploy:
      provider: NuGet
      server:                  # remove to push to NuGet.org
      api_key:
        secure: m49OJ7+Jdt9an3jPcTukHA==
      skip_symbols: false
      symbol_server:           # remove to push symbols to SymbolSource.org
      artifact: /.*\.nupkg/

Your NuGet API key should be encrypted using this tool: https://ci.appveyor.com/tools/encrypt.

## Native packages with CoApp

If you are compiling a native package (such as a C++ library) and need to use
the CoApp Powershell Tools to build the NuGet packages then this can also be
done, but requires a little more work.

The `install` script should be used to download and install the CoApp package.

    # Download the CoApp tools.
    $msiPath = "$($env:USERPROFILE)\CoApp.Tools.Powershell.msi"
    (New-Object Net.WebClient).DownloadFile('http://coapp.org/files/CoApp.Tools.Powershell.msi', $msiPath)
    
    # Install the CoApp tools from the downloaded .msi.
    Start-Process -FilePath msiexec -ArgumentList /i, $msiPath, /quiet -Wait
    
    # Make the tools available for later PS scripts to use.
    $env:PSModulePath = $env:PSModulePath + ';C:\Program Files (x86)\Outercurve Foundation\Modules'
    Import-Module CoApp

Normally, the CoApp tools use an `.autopkg` file rather than a `.nuspec` file to
contain the instructions for building the package.  In the Appveyor environment,
this has one small drawback.  Since the package is built on each push to the
underlying code repository, multiple builds will happen with the same version
number in the `.autopkg` file.  This causes deployment to fail, as a package
cannot be uploaded with the same version number as the existing package.

To solve this, the `.autopkg` file can be renamed to `.autopkg.template`, and a
placeholder used where the version number should go.  The Appveyor build process
can then replace this placeholder with the build number, ensuring the generated
`.autopkg` file always has a unique, incrementing version number.

    nuget {
      nuspec {
        id = example;
        // "@version" is replaced by the current Appveyor build number in the
        // pre-deployment script.
        version: @version;
        title: example;
        ...

All this can be achieved in the `before_deploy` script, which will also use the
CoApp tools to create the `.nupkg` files just before the attempt to deploy them:

    # This is the CoApp .autopkg file to create.
    $autopkgFile = "example.autopkg"
    
    # Get the ".autopkg.template" file, replace "@version" with the Appveyor version number, then save to the ".autopkg" file.
    cat ($autopkgFile + ".template") | % { $_ -replace "@version", $env:appveyor_build_version } > $autopkgFile
    
    # Use the CoApp tools to create NuGet native packages from the .autopkg.
    Write-NuGetPackage $autopkgFile
    
    # Push all newly created .nupkg files as Appveyor artifacts for later deployment.
    Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

The standard NuGet publishing process above can then be used to deploy these
packages.
