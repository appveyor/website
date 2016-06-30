---
layout: docs
title: Packaging artifacts
---

# Packaging artifacts

The **Artifacts** page in the project settings tells AppVeyor which files and folders should be uploaded to AppVeyor cloud storage during the build.  Artifacts can be later deployed to other environments, however deployment is not possible unless a file is listed as an artifact first.

The artifact path must be relative to the root of repository. For example, to upload the `myproject.dll` assembly from the `bin` folder of a project enter:

    bin\debug\myproject.dll

You can use wildcards and environment variables in the artifact path. Let's say the "configuration" variable contains the current build configuration. Then to upload all assemblies in the `bin` directory:

    bin\$(configuration)\*.dll

To push the entire `bin` folder as a single zip archive:

    bin

To push all `*.nupkg` files in the build folder recursively:

    **\*.nupkg

To push all `*.nupkg` files in sub-directory recursively:

    subdir\**\*.nupkg

To configure project artifacts in `appveyor.yml`, use this syntax:

    artifacts:
      - path: test.zip
        name: MyApp

      - path: logs
        name: test logs
        type: zip

> IMPORTANT! If the artifact path starts with `*` surround the value with single quotes, for example:

    - path: '*.nupkg'

or

    - path: '**\*.nupkg' # find all NuGet packages recursively

See [appveyor.yml reference](/docs/appveyor-yml) for more details.


## Packaging multiple files in different locations into a single archive

To create a single "zip" artifact with multiple files from different locations you can use `7z` in "after build" script which is already available in `PATH`:

    7z a myapp.zip %APPVEYOR_BUILD_FOLDER%\path\to\bin\*.dll

Specifying the absolute path here is required to remove paths from archive. However, if you need to preserve paths in the archive use relative paths, like:

    7z a myapp.zip path\to\bin\*.dll

Finally, have only "myapp.zip" pushed to artifacts.

    artifacts:
      - path: myapp.zip
        name: MyApp


## Pushing artifacts from scripts

You can use the following command-line to add a file to the list of build artifacts:

    appveyor PushArtifact <file_name>

or using PowerShell:

    Push-AppveyorArtifact <file_name>

For example, to push all NuGet packages from the build folder (non-recursive):

    after_build:
      - ps: Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

The following command pushes the contents of the `app.publish` folder while preserving the directory structure:

    ps: $root = Resolve-Path .\MyApp\bin\Debug\app.publish; [IO.Directory]::GetFiles($root.Path, '*.*', 'AllDirectories') | % { Push-AppveyorArtifact $_ -FileName $_.Substring($root.Path.Length + 1) -DeploymentName to-publish }

See [Pushing artifacts from scripts](/docs/build-worker-api#push-artifact) for more details.

## Getting information about uploaded artifacts

After all artifacts are uploaded and *before* starting deployment, AppVeyor adds into PowerShell context `$artifacts` hash table with all artifacts. The key of this hash table is the artifact *deployment name* and the value is an object with the following fields:

* `name` - artifact deployment name. GUID if was not specified;
* `type` - artifact type;
* `path` - local artifact path;
* `url` - temporary download URL which is valid for 10 minutes.

You can iterate through all elements of `$artifacts` hash table with the following code:

foreach($artifactName in $artifacts.keys) {
  $artifacts[$artifactName]
}

## Permalink to the last successful build artifact

URL for fetching "last successful" artifact:

    https://ci.appveyor.com/api/projects/<account>/<project>/artifacts/<artifact_file_path>

URL parameters:

* `branch` - if not specified the most recent successful build of *any branch* is fetched.
* `job` - the name of the job. If a build contains multiple jobs then this parameter is mandatory. Value must be URL-encoded, for example `Configuration%3DRelease`.
* `all` - lookup for artifact in not only successful builds, but in `successful`, `failed` and `cancelled` ones. Default is `false`.

Examples:

Downloading artifact from last successful build of any branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip

Downloading artifact for last successful build of `master` branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?branch=master

Downloading artifact from last successful build of `master` branch and "Release" job:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?branch=master&job=Configuration%3A+Release

Downloading artifact from any successful/failed/cancelled build of any branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?all=true