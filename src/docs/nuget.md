---
layout: docs
title: NuGet
---

<!-- markdownlint-disable MD022 MD032 -->
# NuGet
{:.no_toc}

AppVeyor has built-in NuGet hosting. Every AppVeyor account comes with the following feeds:

* **Account feed** - password-protected feed that aggregates NuGet packages from all projects and supports publishing of your own packages.
* **Project feeds** - every project has a NuGet feed which stores all artifact packages of type "NuGet package" pushed during builds.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->


## Account NuGet feed

Account NuGet feed aggregates packages from all project feeds and allows publishing of your custom packages.
All account feeds are password-protected. You can find the account feed URL and its API key on **Account -> NuGet** page.

You can use your AppVeyor account email/password to access password-protected NuGet feeds, although we recommend creating a separate user account just for these purposes (**Account -> Team**).

If you use the GitHub or Bitbucket button to log in to AppVeyor you can reset your AppVeyor account
password using the **Forgot password** link.

For publishing your own packages to your account feed use the command:

     nuget push <your-package.nupkg> -ApiKey <your-api-key> -Source <feed-url>

Replace `<your-api-key>` and `<feed-url>` with values from **Account -> NuGet** page.



## Project NuGet feeds

The project feed contains all build artifact packages of type "NuGet package". If it references a private GitHub or Bitbucket repository the feed is password-protected; otherwise it is public access.



### Automatic publishing of NuGet projects

You can enable automatic publishing of NuGet packages during the build on the project settings **Build** tab. When it is enabled AppVeyor calls `nuget pack` for every project in the solution that has a matching `.nuspec` file with the same name as the project in its root and then publishes NuGet package artifacts in both project and account feeds.

To generate a `.nuspec` file for a project run the following command from within the *project* directory:

    nuget spec



### Pushing NuGet packages from build scripts

To push a NuGet package as an artifact and publish it in both project and account feeds use this command anywhere in your build script:

    appveyor PushArtifact <your-nugetpackage.nupkg>

When you delete a project in AppVeyor its corresponding NuGet feed is deleted,
however all NuGet packages from that feed remain published in account feed.


## Publishing NuGet symbols to AppVeyor account feed

When automatic NuGet packaging is enabled NuGet symbol packages (`*.symbols.nupkg`) are not published to account or project feeds, however, you can setup additional
deployment step to publish NuGet symbols to your AppVeyor account feed:

```yaml
deploy:
  - provider: NuGet
    symbol_server: https://ci.appveyor.com/nuget/<your account feed id>/api/v2/package
    api_key:
      secure: <secure encrypted key here>
    artifact: /.*\.symbols\.nupkg/
```


## Configuring private NuGet feed in Visual Studio

To configure a custom feed in Visual Studio open **Tools -> Options -> Package Manager -> Package Sources** and add new feed.

When you first open the Manage NuGet packages dialog you will be presented with a dialog box asking for credentials:

![nuget environment variables](/assets/images/docs/nuget-visualstudio-auth.png)



## Configuring private NuGet feed from command line

To configure a private NuGet feed on your development machine run this command:

    nuget sources add -Name <friendly-name> -Source <feed-url> -UserName <username> -Password <password>



## Configuring AppVeyor NuGet feeds for your builds

AppVeyor allows you to automatically register *account* and/or *project* private NuGet feeds for the project to make their packages available during the build.

You can enable that through UI on NuGet tab of project settings:

![nuget-project-sources.png](/assets/images/docs/nuget-project-sources.png)

or in `appveyor.yml`:

```yaml
nuget:
  account_feed: true
  project_feed: true
```



## Configuring external private NuGet feed for your builds

To configure an AppVeyor project to use private NuGet feeds during a build you can use the following approach:

1. Create a separate AppVeyor account for accessing NuGet feed.
2. On the **Environment** tab of project settings add the two environment variables, `nuget_user` and `nuget_password`:

   ![nuget environment variables](/assets/images/docs/nuget-environment-variables.png)

3. Into **Install script** box, add this command:

        nuget sources add -Name MyAccountFeed -Source <feed-url> -UserName %nuget_user% -Password %nuget_password%

    where `<feed-url>` is the URL of your [account NuGet feed](https://ci.appveyor.com/nuget).



## Restoring NuGet packages before build

To restore Visual Studio solution NuGet packages before build add this command to the **Before build script** box on the **Build** tab of project settings (provided your `.sln` file or `packages.config` are in the root of repository):

    nuget restore

otherwise, if project solution is in sub-directory:

    nuget restore <solution-folder>\<solution.sln>



## Visual Studio NuGet restore

If you have enabled NuGet Package Restore for Visual Studio solution it will be automatically triggered in the AppVeyor build environment as `EnableNuGetPackageRestore` environment variable is already set to `true`.

## Dealing with intermittent nuget.org issues

Sometimes you may experience issues restoring nuget packages from nuget.org thus failing your builds. We know this is frustrating, but there is something you can do to minimize your dependency on nuget.org availability.

We collected some notes how to better deal with intermittent nuget.org issues.

### Enable detailed logging

If you experience `nuget restore` issues enable detailed logging with `-verbosity detailed` command line option, for example:

    nuget restore -verbosity detailed

This will help to better understand the root cause of the issue.

### Disable Parallel Processing

The restore process is more stable by disabling the parallel processing.

    nuget restore -DisableParallelProcessing

### Download latest NuGet command-line

There is a chance that NuGet issue you are experiencing has been fixed in the latest `nuget.exe` which is available for download.

To download the latest `nuget.exe` add this to "Install script" section of your build (or under `install` in `appveyor.yml`):

    appveyor DownloadFile https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

### Restore with retries

The idea of "reliable" restore is simple - having batch file retrying `nuget restore` few times until the command exits with code 0.

There is such a batch file provided by AppVeyor Build Agent called `appveyor-retry.cmd`.

To use `appveyor-retry` with `nuget restore`:

    appveyor-retry nuget restore

or

    appveyor-retry nuget restore <path-to\solution.sln>


### Use build cache for NuGet packages

To avoid downloading packages from nuget.org on every build you can use [build cache](/docs/build-cache/).

If `packages` folder is in the root of your repo add this to `appveyor.yml`:

```yaml
cache:
  - packages -> **\packages.config
```

That means *"preserve `packages` folder contents between builds unless any of `packages.config` changes"*.

## External links

* [Configuring NuGet server to use Authentication](https://stackoverflow.com/questions/17928112/configuring-nuget-server-to-use-authentication)
* [NuGet Configuration Settings](https://docs.nuget.org/consume/nuget-config-settings)
