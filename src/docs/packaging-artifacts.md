---
layout: docs
title: Packaging artifacts
---

<!-- markdownlint-disable MD022 MD032 -->
# Packaging artifacts
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Basics

The **Artifacts** page in the project settings tells AppVeyor which files and folders should be uploaded to AppVeyor cloud storage during the build.  Artifacts can be later deployed to other environments, however deployment is not possible unless a file is listed as an artifact first.

The artifact path must be relative to the root of repository. For example, to upload the `myproject.dll` assembly from the `bin` folder of a project enter:

    bin\debug\myproject.dll

You can use wildcards and environment variables in the artifact path. Let's say the "configuration" variable contains the current build configuration. Then to upload all assemblies in the `bin` directory:

    bin\$(configuration)\*.dll

To push the entire `bin` folder as a single zip archive:

    bin

To push all `*.nupkg` files in the build folder recursively:

    **\*.nupkg

To push all `*.nupkg` files in the sub-directory recursively:

    subdir\**\*.nupkg

To configure project artifacts in `appveyor.yml`, use this syntax:

```yaml
artifacts:
  - path: test.zip
    name: MyApp

  - path: logs
    name: test logs
    type: zip
```

IMPORTANT! If the artifact path starts with `*`, you need to surround the value with single quotes, for example:

```yaml
- path: '*.nupkg'
```

or

```yaml
- path: '**\*.nupkg' # find all NuGet packages recursively
```

The following artifact types are supported:

* `Auto` (default) - infer type automatically from file extension
* `WebDeployPackage` - Web Deploy package with `.zip` extension
* `NuGetPackage` - `.nupkg` files
* `AzureCloudService` - `.cspkg` files
* `AzureCloudServiceConfig` - `.cscfg` files
* `SsdtPackage`- `.dacpac` files
* `Zip` - `.zip` files
* `File` - any other file types

See [appveyor.yml reference](/docs/appveyor-yml/) for more details.


## Packaging multiple files in different locations into a single archive

To create a single "zip" artifact with multiple files from different locations you can use `7z` in "after build" script which is already available in `PATH`:

    7z a myapp.zip %APPVEYOR_BUILD_FOLDER%\path\to\bin\*.dll

Specifying the absolute path here is required to remove paths from archive. However, if you need to preserve paths in the archive use relative paths, like:

    7z a myapp.zip path\to\bin\*.dll

Finally, have only "myapp.zip" pushed to artifacts.

```yaml
artifacts:
  - path: myapp.zip
    name: MyApp
```

## Pushing artifacts from scripts

You can use the following command-line to add a file to the list of build artifacts:

    appveyor PushArtifact <file_name>

or using PowerShell:

    Push-AppveyorArtifact <file_name>

For example, to push all NuGet packages from the build folder (non-recursive):

```yaml
after_build:
  - ps: Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
```

The following command pushes the contents of the `app.publish` folder while preserving the directory structure:

```yaml
ps: $root = Resolve-Path .\MyApp\bin\Debug\app.publish; [IO.Directory]::GetFiles($root.Path, '*.*', 'AllDirectories') | % { Push-AppveyorArtifact $_ -FileName $_.Substring($root.Path.Length + 1) -DeploymentName to-publish }
```

See [Pushing artifacts from scripts](/docs/build-worker-api#push-artifact) for more details.

## Getting information about uploaded artifacts

After all artifacts are uploaded and *before* starting deployment, AppVeyor adds into PowerShell context `$artifacts` hash table with all artifacts. The key of this hash table is the artifact *deployment name* and the value is an object with the following fields:

* `name` - artifact deployment name. GUID if was not specified;
* `type` - artifact type;
* `path` - local artifact path;
* `url` - temporary download URL which is valid for 10 minutes.

You can iterate through all elements of `$artifacts` hash table with the following code:

```powershell
foreach ($artifactName in $artifacts.keys) {
  $artifacts[$artifactName]
}
```

## Permalink to the last successful build artifact

Artifacts may be fetched by URL, bear in mind authentication is required, see [AppVeyor REST API](/docs/api/#authentication) for more info. The URL for fetching "last successful" artifact:

    https://ci.appveyor.com/api/projects/<account>/<project>/artifacts/<artifact_file_path>

URL parameters:

* `branch` - if not specified the most recent successful build of *any branch* is fetched.
* `tag` - if not specified the most recent successful build of *any tag* is fetched.
* `job` - the name of the job. If a build contains multiple jobs then this parameter is mandatory. Value must be URL-encoded, for example `Configuration%3DRelease`.
* `all` - lookup for artifact in not only successful builds, but in `successful`, `failed` and `cancelled` ones. Default is `false`.
* `pr` - include PR builds in the search results. `true` - take artifact from PR builds only, `false` - do not look for artifact in PR builds; otherwise look for artifact in both PR an non-PR builds.

Examples:

Downloading an artifact from the last successful build of any branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip

Downloading an artifact for the last successful build of the `master` branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?branch=master

Downloading an artifact from the last successful build of the `master` branch and the "Release" job:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?branch=master&job=Configuration%3A+Release

Downloading an artifact for the last successful build of `1.1` tag:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?tag=1.1

Downloading an artifact from any successful/failed/cancelled build of any branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?all=true

Downloading an artifact from the last successful non-PR build of any branch:

    https://ci.appveyor.com/api/projects/johnsmith/myproject/artifacts/bin/debug.zip?pr=false


## Artifacts retention policy

The purpose of artifacts, storage, not for archival porposes

AppVeyor implements artifacts retention policy for both private and public projects:

* Artifacts older than 6 months are permanently removed from AppVeyor artifact storage.
* NuGet packages on both project and accounts feeds are not affected by the policy.

> It's responsibility of project maintainers to copy critical artifacts that maybe useful after 6 months to an external storage.

### Copying artifacts to an external storage during the build

You can configure [inline](/docs/deployment/#overview) (run during the build) deployment to copy artifacts
to your own [FTP](/docs/deployment/ftp/), [Azure](/docs/deployment/azure-blob/), [S3](/docs/deployment/amazon-s3/), [Bintray](/docs/deployment/bintray/) or [GitHub Releases](/docs/deployment/github/) storage.

For example, to copy *all artifacts* from the running build to Amazon S3 storage add the following to your `appveyor.yml`:

```yaml
deploy:
  provider: S3
  access_key_id:
    secure: <encrypted-access-key-id>
  secret_access_key:
    secure: <encrypted-access-key-secret>
  bucket: <your-bucket>
  folder: $(APPVEYOR_PROJECT_SLUG)/$(APPVEYOR_BUILD_VERSION)
```

Sensitive deployment parameters can be encrypted with [Encrypt data tool](https://ci.appveyor.com/tools/encrypt).

> Note how variables are used in `folder` parameter - this allows reusing YAML snippet across mulitple projects while making sure project artifacts are copied to separate folders.

### Copying artifacts of the finished builds to an external storage

You can use [Environments](https://ci.appveyor.com/environments) deployment to export (deploy) existing artifacts.

In the example below we will setup Azure Blob Storage account to copy artifact to.

Go to [Environments](https://ci.appveyor.com/environments) page and click **New environment** button.

Select **Azure Blob Storage** provider and fill the settings:

* Environment name: `Artifacts archive`
* Storage account name: `<azure-storage-account-name>`
* Storage access key: `<azure-storage-access-key>`
* Container name: `my-artifacts`
* Folder: `$(APPVEYOR_PROJECT_SLUG)/latest`

Click **Add environment** button to save the changes.

Now go to the project build details which artifacts you'd like to export and click **Deploy** button.

Select **Artifacts archive** environment and click **Deploy**.

Repeat deployment for other projects/builds.

### Re-build last successful commit

If the artifact was already expired and removed by AppVeyor you can re-run previous build and produce the artifact again.

To re-build certain commit open project history page and go to the build details of required build.

Click **Re-build commit** button.