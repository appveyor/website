---
title: NuGet support in AppVeyor CI
---

<em>**NOTE**: NuGet support is available in AppVeyor CI 2.0 which is currently in beta. Please <a title="AppVeyor 2.0: dedicated build VMs, parallel testing, NuGet, deployment and more" href="/blog/2014/02/19/appveyor-20-dedicated-build-vms-parallel-testing-nuget-deployment/">see this post</a> for AppVeyor 2.0 announcement and sign up information.</em>

AppVeyor CI has native NuGet support which becomes de-facto a packaging standard for .NET libraries and applications.

Every AppVeyor account comes with following built-in NuGet feeds:

* **Account feed** - password-protected feed aggregating NuGet packages from all projects with support of publishing of your own packages
* **Project feeds** - collect all NuGet packages pushed to artifacts during the build

## Account NuGet feed

Account NuGet feed aggregates packages from all project feeds and allows publishing of your custom packages. All account feeds are password-protected. You can find account feed URL and its API key on **Account → NuGet** page:

<img alt="nuget-account" src="/assets/images/posts/nuget-support/nuget-account.png" width="584" height="305">

You can use your AppVeyor account email/password to access password-protected NuGet feeds although we recommend creating a separate user account just for these purposes (**Account → Team**).

> If you use GitHub or BitBucket button to login AppVeyor you can reset your AppVeyor account password usingForgot password link.

For publishing your own packages to account feed use the command:

```text
nuget push <your-package.nupkg> -ApiKey <your-api-key> -Source <feed-url>
```

Replace `<your-api-key>` and `<feed-url>` with values from Account **→** NuGet page.

## Project NuGet feed

Project feed collects all NuGet packages pushed to artifacts during the build. Project feed is password-protected if the project references private GitHub or BitBucket repository; otherwise project feed has public access:

<img alt="nuget-project-feed" src="/assets/images/posts/nuget-support/nuget-project-feed1.png" width="584" height="296">

### Automatic publishing of NuGet projects

You can enable automatic publishing of NuGet packages during the build on **Build** tab of project settings. When it is enabled AppVeyor calls `nuget pack` for every project in the solution having `.nuspec` file in the root and then publishes NuGet package artifact in both project and account feeds.

To generate a `.nuspec` file for your project run the following command from project root directory:

```text
nuget spec
```

### Pushing NuGet packages from build scripts

To push NuGet package as artifact and publish it in both project and account feeds use this command anywhere in your build script:

```text
appveyor PushArtifact <your-nugetpackage.nupkg>
```

> When you delete a project in AppVeyor its corresponding NuGet feed is deleted, however all NuGet packages from that feed remain published in account feed.

## Configuring private NuGet feed on your development machine

### Visual Studio

To configure custom feed in Visual Studio open **Tools → Options → Package Manager → Package Sources** and add new feed.

When you first open Manage NuGet packages dialog you will be presented with a dialog box asking for credentials:

<img alt="nuget-visualstudio-auth" src="/assets/images/docs/nuget-visualstudio-auth.png" width="584" height="389">

### Command line

To configure private NuGet feed on your development machine run this command:

```text
nuget sources add -Name <friendly-name> -Source <feed-url> -UserName <username> -Password <pass>
```

## Configuring private feed to work with NuGet Package Restore

You may use account feed to publish your external packages that can be further referenced during AppVeyor builds.

To configure AppVeyor project to use private NuGet feed during build you can use the following approach:

1. Create a separate AppVeyor account for accessing NuGet feed.
2. On **Environment** tab of project settings add two environment variables `nuget_user` and `nuget_password`:
   <img alt="nuget-environment-variables" src="/assets/images/docs/nuget-environment-variables.png" width="584" height="114">
3. Into **Install** script box add this command:

    ```text
    nuget sources add -Name MyAccountFeed -Source <feed-url> -UserName %nuget_user% -Password %nuget_password%
    ```

Replace `<feed-url>` with URL of your account feed.

## Explicit NuGet package restore before the build

To restore Visual Studio solution NuGet packages before build add this command to **Before build script** box on **Build** tab of project settings (provided your `.sln` file or `packages.config` are in the root of repository):

```text
nuget restore
```

otherwise if project solution is in sub-directory:

```text
nuget restore <solution-folder>\<solution.sln>
```
