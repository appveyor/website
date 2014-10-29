---
layout: docs
title: Packaging artifacts
---

# Packaging artifacts

The **Artifacts** page of project settings tells AppVeyor which files and folders should be uploaded to AppVeyor cloud storage during the build.

The artifact path must be relative to the root of repository. For example, to upload `myproject.dll` assembly from `bin` folder of a project enter:

	myproject\bin\debug\myproject.dll

You can use wildcards and environment variables in the artifact path. Let's say the "configuration" variable contains the current build configuration. Then to upload all assemblies in bin directory:

	myproject\bin\$(configuration)\*.dll

To push the entire `bin` folder as a single zip archive:

	myproject\bin

To push all `*.nupkg` files in the build folder recursively:

	*.nupkg

To configure project artifacts in `appveyor.yml`:

    artifacts:
      - path: test.zip
        name: MyApp

      - path: logs
        name: test logs
        type: zip

> IMPORTANT! If artifact path starts with `*` surround the value into single quotes, for example:

    - path: '*.nupkg'

or

    - path: '**\*.nupkg' # find all NuGet packages recursively

See [appveyor.yml refence](/docs/appveyor-yml) for more details.

## Pushing artifacts from scripts

You can use the following command-line to push file to build artifacts:

    appveyor PushArtifact <file_name>

or using PowerShell:

    Push-AppveyorArtifact <file_name>

For example, to push all NuGet packages from build folder (non-recursive):

    after_build:
      - ps: Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

The following command pushes the contents of `app.publish` folder preserving directories structure:

    ps: $root = Resolve-Path .\MyApp\bin\Debug\app.publish; [IO.Directory]::GetFiles($root.Path, '*.*', 'AllDirectories') | % { Push-AppveyorArtifact $_ -FileName $_.Substring($root.Path.Length + 1) -DeploymentName to-publish }

See [Pushing artifacts from scripts](/docs/build-agent-api#push-artifact) for more details.
